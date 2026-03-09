using System;
using System.Text;
using RabbitMQ.Client;
using CodeSectorCMS.Domain.Messages;
using CodeSectorCMS.Domain.MessageModels;

namespace CodeSectorCMS.Domain.Managers.Implementations
{
    public class RabbitMq : IMessagePublisher
    {
        private static ConnectionFactory mqFactory = new ConnectionFactory { HostName = "localhost" };

        private string prefix = @".\Private$\";
        private string _queueName;

        // Queue constructor        
       public RabbitMq(string queueName)
       {
            _queueName = prefix + queueName;
       }

        // Recieving message from queue
        public async Task<CreatedMessage> Recieve()
        {
            CreatedMessage res = new CreatedMessage();

            using (var connection = await mqFactory.CreateConnectionAsync())
            using (var channel = await connection.CreateChannelAsync())
            {
                await channel.QueueDeclareAsync(queue: _queueName,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

                // Get a single message
                var result = channel.BasicGetAsync(_queueName, autoAck: false);

                if (result.Result != null)
                {
                    byte[] body = result.Result.Body.ToArray();
                    string message = Encoding.UTF8.GetString(body);
                    
                    res = System.Text.Json.JsonSerializer.Deserialize<CreatedMessage>(message);

                    // Acknowledge the message to remove it from the queue
                    await channel.BasicAckAsync(result.Result.DeliveryTag, multiple: false);
                }
                else
                {
                    Console.WriteLine("No messages in the queue.");
                }

                return res;
            }
        }

        public async Task Publish(CreatedMessage message)
        {
            using (var connection = await mqFactory.CreateConnectionAsync())
            using (var channel = await connection.CreateChannelAsync())
            {
                await channel.QueueDeclareAsync(queue: _queueName,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

                var json = System.Text.Json.JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);

                await channel.BasicPublishAsync(exchange: string.Empty, // Default exchange
                                     routingKey: _queueName, // The queue name acts as the routing key
                                     mandatory: true,
                                     basicProperties: new BasicProperties { Persistent = true },
                                     body: body);
            }
        }
    }
}
