using System;

namespace CodeSectorCMS.Domain.Managers.Interfaces
{
    public interface ISubscriberGroupManager : IDisposable
    {
        List<SubscriberGroup> GetAllSubscriberGroups(int ClientId);
        void CreateNewSubscriberGroup(SubscriberGroup subscriberGroup);
        SubscriberGroup GetSubscriberGroupByID(int ClientId, int SubscriberGroupId);
        SubscriberGroup GetSubscriberGroupWithSubscribersByID(int ClientId, int SubscriberGroupId);
        void DeleteSubscriberGroupByID(int SubscriberGroupId);
        void SaveChanges();
    }
}
