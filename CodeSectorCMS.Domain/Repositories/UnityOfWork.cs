using System;

namespace CodeSectorCMS.Domain.Repositories
{
    public class UnityOfWork : IDisposable
    {
        private readonly CmsContext context;

        public UnityOfWork(CmsContext dbContext)
        {
            context = dbContext;
        }

        private GenericRepository<APIKey> apikeyRepository;
        private GenericRepository<Message> messageRepository;
        private GenericRepository<Campaign> campaignRepository;
        private GenericRepository<Template> templateRepository;
        private GenericRepository<SubscriberGroup> subscriberGroupRepository;
        private GenericRepository<MailConfig> mailconfigRepository;
        private GenericRepository<Subscriber> subscriberRepository;
        private GenericRepository<Client> clientRepository;
        private GenericRepository<Account> accountRepository;
        private GenericRepository<CustomField> customFieldRepository;
        private GenericRepository<SubscriberCustomFieldValue> subscriberCustomFieldValueRepository;

        public GenericRepository<APIKey> APIKeyRepository
        {
            get
            {
                if (this.apikeyRepository == null)
                {
                    this.apikeyRepository = new GenericRepository<APIKey>(context);
                }

                return apikeyRepository;
            }
        }


        public GenericRepository<Message> MessageRepository
        {
            get
            {
                if (this.messageRepository == null)
                {
                    this.messageRepository = new GenericRepository<Message>(context);
                }

                return messageRepository;
            }
        
        }

        public GenericRepository<Account> AccountRepository
        {
            get
            {
                if (this.accountRepository == null)
                {
                    this.accountRepository = new GenericRepository<Account>(context);
                }

                return accountRepository;
            }

        }

        public GenericRepository<Client> ClientRepository
        {
            get
            {
                if (this.clientRepository == null)
                {
                    this.clientRepository = new GenericRepository<Client>(context);
                }

                return clientRepository;
            }

        }



        public GenericRepository<Campaign> CampaignRepository
        {
            get
            {
                if (this.campaignRepository == null)
                {
                    this.campaignRepository = new GenericRepository<Campaign>(context);
                }

                return campaignRepository;
            }

        }


        public GenericRepository<Template> TemplateRepository
        {
            get
            {
                if (this.templateRepository == null)
                {
                    this.templateRepository = new GenericRepository<Template>(context);
                }

                return templateRepository;
            }

        }


        public GenericRepository<SubscriberGroup> SubscriberGroupRepository
        {
            get
            {
                if (this.subscriberGroupRepository == null)
                {
                    this.subscriberGroupRepository = new GenericRepository<SubscriberGroup>(context);
                }

                return subscriberGroupRepository;
            }

        }


        public GenericRepository<MailConfig> MailConfigRepository
        {
            get
            {
                if (this.mailconfigRepository == null)
                {
                    this.mailconfigRepository = new GenericRepository<MailConfig>(context);
                }

                return mailconfigRepository;
            }

        }


        public GenericRepository<Subscriber> SubscriberRepository
        {
            get
            {
                if (this.subscriberRepository == null)
                {
                    this.subscriberRepository = new GenericRepository<Subscriber>(context);
                }

                return subscriberRepository;
            }

        }


        public GenericRepository<CustomField> CustomFieldRepository
        {
            get
            {
                if (this.customFieldRepository == null)
                {
                    this.customFieldRepository = new GenericRepository<CustomField>(context);
                }

                return customFieldRepository;
            }

        }


        public GenericRepository<SubscriberCustomFieldValue> SubscriberCustomFieldValueRepository
        {
            get
            {
                if (this.subscriberCustomFieldValueRepository == null)
                {
                    this.subscriberCustomFieldValueRepository = new GenericRepository<SubscriberCustomFieldValue>(context);
                }

                return subscriberCustomFieldValueRepository;
            }

        }


        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
