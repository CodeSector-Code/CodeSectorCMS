using System;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace CodeSectorCMS.Domain.Repositories
{
    public class SubscriberRepository : ISubscriberRepository, IDisposable
    {
        private CmsContext context;

        public SubscriberRepository(CmsContext context)
        {
            this.context = context;
        }

        public IEnumerable<Subscriber> GetAllSubscribers()
        {
            return context.Subscribers.ToList();
        }

        public void InsertSubscriber(Subscriber subscriber)
        {
            context.Subscribers.Add(subscriber);
        }

        public void DeleteSubscriber(int subscriberID)
        {
            Subscriber subscriber = context.Subscribers.Find(subscriberID);
            context.Subscribers.Remove(subscriber);
        }

        public void UpdateSubscriber(Subscriber subscriber)
        {
            context.Entry(subscriber).State = EntityState.Modified;
        }

        public IEnumerable<Subscriber> FindByName(string name)
        {
            return context.Subscribers.Where(c => c.Name.StartsWith(name)).ToList();
        }

        public Subscriber GetSubscriberByID(int subscriberID)
        {
            return context.Subscribers.Find(subscriberID);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
