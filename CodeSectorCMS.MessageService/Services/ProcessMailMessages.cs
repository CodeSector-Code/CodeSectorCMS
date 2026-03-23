using System;
using System.Text.Json;
using CodeSectorCMS.Domain;
using Azure.Messaging.ServiceBus;
using CodeSectorCMS.Domain.MessageModels;
using CodeSectorCMS.Domain.Managers.Interfaces;

namespace CodeSectorCMS.MessageService.Services
{
    public class ProcessMailMessages : BackgroundService
    {
        private const string QueueName = "mails";
        
        private readonly ServiceBusClient serviceBus;
        private readonly ServiceBusProcessor serviceBusProcessor;

        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly ILogger<ProcessMailMessages> logger;

        public ProcessMailMessages(ILogger<ProcessMailMessages> logger,
            ServiceBusClient serviceBus,
            IServiceScopeFactory serviceScopeFactory)
        {
            this.logger = logger;
            this.serviceBus = serviceBus;
            this.serviceScopeFactory = serviceScopeFactory;

            serviceBusProcessor = serviceBus.CreateProcessor(QueueName);

            serviceBusProcessor.ProcessMessageAsync += args => ProcessMessageAsync(args);
            serviceBusProcessor.ProcessErrorAsync += ProcessErrorAsync;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var queueTask = ConsumeFromQueueAsync(stoppingToken);

            await queueTask;
        }

        private async Task ConsumeFromQueueAsync(CancellationToken stoppingToken)
        {
            await serviceBusProcessor.StartProcessingAsync(stoppingToken);
        }

        private async Task ProcessMessageAsync(ProcessMessageEventArgs args)
        {
            try
            {
                var messageBody = args.Message.Body.ToString();
                var message = JsonSerializer.Deserialize<CreatedMessage>(messageBody);

                using (IServiceScope scope = serviceScopeFactory.CreateScope())
                {
                    var mailService = scope.ServiceProvider.GetRequiredService<IMailService>();
                    var trackMessageManager = scope.ServiceProvider.GetRequiredService<ITrackMessageManager>();
                    var mailConfigManager = scope.ServiceProvider.GetRequiredService<IMailConfigManager>();

                    // Retrieve client main configuration and send an email
                    MailConfig clientMailConfig = mailConfigManager.GetMailConfigByID(message.MailConfigId);
                    mailService.SendMail(clientMailConfig, message, message.Email);

                    TrackMessage trMessage = trackMessageManager.GetTrackedMessageByMessageId(message.MessageID);
                    trMessage.Sent = true;
                    
                    trackMessageManager.SaveTrackMessage(trMessage);
                    trackMessageManager.SaveChanges();
                }

            }
            catch (Exception ex)
            {

                logger.LogError(ex, "Error processing queue message");
                await args.AbandonMessageAsync(args.Message);
            }
        }

        private Task ProcessErrorAsync(ProcessErrorEventArgs arg)
        {
            logger.LogError(arg.Exception, "Service Bus error: {ErrorSource}", arg.ErrorSource);
            return Task.CompletedTask;
        }
    }
}
