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
            // Get User mail configuration (we use first configuration for now)
            //var user = Unit.UserRepository.Get(includeProperties: "MailConfigs").Where(x => x.UserId == request.UserId).First();
            //var mailConfig = user.MailConfigs.First();

            // Create new campaign and save it to db
            Campaign campaign = new Campaign
            {
                Name = request.CampaignName,
                Description = request.CampaignDescription,
                UserId = (request.UserId ?? 1),
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

            // Get a list of all custom fields created by this user
            var userCustomFields = Unit.UserRepository.Get(includeProperties: "CustomFields").Where(x => x.UserId == campaign.UserId).First().CustomFields.ToList();

            // Create and save a message for every subscriber in the group using 
            // template for this campaign
            foreach (Subscriber subscriber in subscribers)
            {
                // Get values from payload join table for subscribers and custom fields
                var scfValues = Unit.SubscriberRepository.Get(includeProperties: "SubscriberCustomFieldValues").Where(x => x.SubscriberID == subscriber.SubscriberID).First().SubscriberCustomFieldValues.ToList();

                // Create message body and subject
                string messageBody = FillCustomFields(template.Body, userCustomFields, scfValues, subscriber);
                string messageSubject = FillCustomFields(template.Subject, userCustomFields, scfValues, subscriber);

                // Create new Message entity ...
                Message message = CreateNextMessage(messageBody, campaign.CampaignID);

                // Add it to database and then forward to message queue
                Unit.MessageRepository.Insert(message);
                Unit.Save();

                TrackMessage trackMessage = CreateTrackMessage(message.MessageID, messageSubject, subscriber.Email, subscriber.Name);
                Unit.TrackMessageRepository.Insert(trackMessage);
                

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

                messagePublisher.Publish(messageForQueue);
            }
        }

        private Message CreateNextMessage(string messageBody, int campaignID)
        {
            var message = new Message
            {
                CampaignID = campaignID,
                Body = messageBody,
                SentFLAG = false,
                MonitoringStatus = MonitoringSTATUS.unknown
            };

            return message;
        }

        private TrackMessage CreateTrackMessage(int id, string subject, string receiver, string company)
        {
            var message = new TrackMessage
            {
                MessageId = id,
                Subject = subject,
                Receiver = receiver,
                Company = company
            };

            return message;
        }

        // Function that inserts appropriate custom field values into the template body
        private string FillCustomFields(string templateBody, List<CustomField> userCustomFields,
            List<SubscriberCustomFieldValue> scfValues, Subscriber subscriber)
        {
            string messageBody = templateBody;

            // First replace occurences of three default custom fields ...
            messageBody = messageBody.Replace("*!*Name*!*", subscriber.Name);
            messageBody = messageBody.Replace("*!*Email*!*", subscriber.Email);
            messageBody = messageBody.Replace("*!*PhoneNumber*!*", subscriber.PhoneNumber);

            // Replace every occurence custom field with appropriate value
            foreach (CustomField customField in userCustomFields)
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
