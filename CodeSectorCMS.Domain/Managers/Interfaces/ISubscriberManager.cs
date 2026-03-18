using System;

namespace CodeSectorCMS.Domain.Managers.Interfaces
{
     public interface ISubscriberManager : IDisposable
    {
         List<Subscriber> GetAllSubscribers(int UserId);
         List<Subscriber> GetAllSubscribersWithCustomFieldValues(int UserId);
         void CreateNewSubscriber(Subscriber Subscriber);
         Subscriber GetSubscriberByID(int UserId, int SubscriberId);
         void DeleteSubscriberByID(int UserId, int SubscriberId);
         void UpdateSubscriber(Subscriber subscriber);
         void SaveChanges();

    }
}
