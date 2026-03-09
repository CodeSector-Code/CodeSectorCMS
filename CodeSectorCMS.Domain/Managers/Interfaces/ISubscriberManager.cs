using System;

namespace CodeSectorCMS.Domain.Managers.Interfaces
{
     public interface ISubscriberManager : IDisposable
    {
         List<Subscriber> GetAllSubscribers(int ClientId);
         List<Subscriber> GetAllSubscribersWithCustomFieldValues(int ClientId);
         void CreateNewSubscriber(Subscriber Subscriber);
         Subscriber GetSubscriberByID(int ClientId, int SubscriberId);
         void DeleteSubscriberByID(int ClientId, int SubscriberId);
         void UpdateSubscriber(Subscriber subscriber);
         void SaveChanges();

    }
}
