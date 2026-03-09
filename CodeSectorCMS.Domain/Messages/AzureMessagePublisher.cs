using System;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using CodeSectorCMS.Domain.MessageModels;

namespace CodeSectorCMS.Domain.Messages
{
    public class AzureMessagePublisher : IMessagePublisher
    {
        private const string QueueName = "mails";

        private readonly ServiceBusClient serviceBus;

        public AzureMessagePublisher(ServiceBusClient serviceBus) 
        {
            this.serviceBus = serviceBus;
        }

        public async Task Publish(CreatedMessage message)
        {
            var messageBody = JsonSerializer.Serialize(message);

            var azureMessage = new ServiceBusMessage(messageBody)
            {
                MessageId = message.MessageID.ToString(),
                Subject = message.Subject,
            };

            // Publish to the Queue

            await using var queueSender = serviceBus.CreateSender(QueueName);
            await queueSender.SendMessageAsync(azureMessage);
        }
    }
}
