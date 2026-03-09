using System;

namespace CodeSectorCMS.Domain.Repositories
{
    public interface ISubscriberRepository : IDisposable
    {
        IEnumerable<Subscriber> GetAllSubscribers();
        Subscriber GetSubscriberByID(int SubscriberID);
        IEnumerable<Subscriber> FindByName(string name);
        void InsertSubscriber(Subscriber subscriber);
        void DeleteSubscriber(int SubscriberID);
        void UpdateSubscriber(Subscriber subscriber);
        void Save();
    }
}
