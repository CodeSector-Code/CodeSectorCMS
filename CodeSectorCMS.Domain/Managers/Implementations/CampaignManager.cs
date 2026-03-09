using System;
using Microsoft.Extensions.Options;

// Our namespaces
using CodeSectorCMS.Domain;
using CodeSectorCMS.Domain.MessageModels;
using CodeSectorCMS.Domain.Repositories;
using CodeSectorCMS.Domain.Managers.Interfaces;
using CodeSectorCMS.Domain.Messages;

namespace CodeSectorCMS.Domain.Managers.Implementations
{
    public class CampaignManager : ICampaignManager
    {
        private UnityOfWork Unit;
        private readonly IMessagePublisher messagePublisher;

        public CampaignManager(IMessagePublisher messagePublisher, UnityOfWork unityOfWork)
        {
            this.messagePublisher = messagePublisher;
            Unit = unityOfWork;
        }

        public void CreateNewCampaign(Request request)
        {
            // Get client mail configuration (we use first configuration for now)
            //var client = Unit.ClientRepository.Get(includeProperties: "MailConfigs").Where(x => x.ClientID == request.ClientID).First();
            //var mailConfig = client.MailConfigs.First();

            // Create new campaign and save it to db
            Campaign campaign = new Campaign
            {
                Name = request.CampaignName,
                Description = request.CampaignDescription,
                ClientID = (request.ClientID ?? 1),
                AccountID = (request.AccountID ?? 1),
                TemplateID = (request.TemplateID ?? 1),
                SubscriberGroupID = (request.SubsrciberGroupID ?? 1),
                MailConfigID = request.MailConfigId
            };
            Unit.CampaignRepository.Insert(campaign);
            Unit.Save(); // to fix Fk_Messages_Campaign_CampaignId update error
                         // 
            ProcessCampaignMessages(campaign);
        }

        public List<Campaign> GetAllCampaigns()
        {
            return Unit.CampaignRepository.GetAll().ToList();
        }

        public Campaign GetCampaignByID(int id)
        {
            return Unit.CampaignRepository.GetByID(id);
        }

        public void SaveCampaign(Campaign campaign)
        {
            Unit.CampaignRepository.Update(campaign);
        }

        public void RemoveCampaignByID(int id)
        {
            Campaign campaign = Unit.CampaignRepository.GetByID(id);

            Unit.CampaignRepository.Delete(campaign);
        }

        public void SaveChanges()
        {
            Unit.Save();
        }

        public void Dispose()
        {
            Unit.Dispose();
        }

        private void ProcessCampaignMessages(Campaign campaign)
        {
            // Read subscribers from info from the database
            var subscriberGroup = Unit.SubscriberGroupRepository.Get(includeProperties: "Subscribers").Where(s => s.SubscriberGroupID == campaign.SubscriberGroupID).First();
            var subscribers = subscriberGroup.Subscribers.ToList();

            // Get template associated with this campaign
            Template template = Unit.TemplateRepository.GetByID(campaign.TemplateID);

            // Get a list of all custom fields created by this client
            var clientCustomFields = Unit.ClientRepository.Get(includeProperties: "CustomFields").Where(x => x.ClientID == campaign.ClientID).First().CustomFields.ToList();

            int numberOfMessages = Unit.MessageRepository.GetAll().Count();
            int counter = 1;

            // Create and save a message for every subscriber in the group using 
            // template for this campaign
            foreach (Subscriber subscriber in subscribers)
            {
                // Get values from payload join table for subscribers and custom fields
                var scfValues = Unit.SubscriberRepository.Get(includeProperties: "SubscriberCustomFieldValues").Where(x => x.SubscriberID == subscriber.SubscriberID).First().SubscriberCustomFieldValues.ToList();

                // Create message body and subject
                string messageBody = FillCustomFields(template.Body, clientCustomFields, scfValues, subscriber);
                string messageSubject = FillCustomFields(template.Subject, clientCustomFields, scfValues, subscriber);

                // Create new Message entity ...
                Domain.Message message = CreateNextMessage(messageBody, campaign.CampaignID, numberOfMessages + counter);

                // .. and a message for message queue
                CreatedMessage messageForQueue = new CreatedMessage
                {
                    MessageID = message.MessageID,
                    Email = subscriber.Email,
                    Subject = messageSubject,
                    Body = message.Body,
                    MonitoringStatus = Domain.MonitoringSTATUS.unknown,
                    SentFLAG = message.SentFLAG,
                    MailConfigId = campaign.MailConfigID
                };

                // Add it to database and then forward to message queue
                Unit.MessageRepository.Insert(message);
                messagePublisher.Publish(messageForQueue);
                counter++;
            }
            Unit.Save();
        }

        private Message CreateNextMessage(string messageBody, int campaignID, int messageID)
        {
            var message = new Message
            {
                MessageID = messageID,
                CampaignID = campaignID,
                Body = messageBody,
                SentFLAG = false,
                MonitoringStatus = MonitoringSTATUS.unknown
            };

            return message;
        }

        // Function that inserts appropriate custom field values into the template body
        private string FillCustomFields(string templateBody, List<CustomField> clientCustomFields,
            List<SubscriberCustomFieldValue> scfValues, Subscriber subscriber)
        {
            string messageBody = templateBody;

            // First replace occurences of three default custom fields ...
            messageBody = messageBody.Replace("*!*Name*!*", subscriber.Name);
            messageBody = messageBody.Replace("*!*Email*!*", subscriber.Email);
            messageBody = messageBody.Replace("*!*PhoneNumber*!*", subscriber.PhoneNumber);

            // Replace every occurence custom field with appropriate value
            foreach (CustomField customField in clientCustomFields)
            {
                string oldValue = "*!*" + customField.Name + "*!*";
                string newValue = (scfValues.Where(s => s.SubscriberID == subscriber.SubscriberID && s.CustomFieldID == customField.CustomFieldID)
                                            .Select(s => s.Value)).FirstOrDefault();
                messageBody = messageBody.Replace(oldValue, newValue);
            }

            return messageBody;
        }
    }
}
