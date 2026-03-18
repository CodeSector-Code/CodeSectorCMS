using System;

namespace CodeSectorCMS.Domain.Managers.Interfaces
{
    public interface ISubscriberGroupManager : IDisposable
    {
        List<SubscriberGroup> GetAllSubscriberGroups(int UserId);
        void CreateNewSubscriberGroup(SubscriberGroup subscriberGroup);
        SubscriberGroup GetSubscriberGroupByID(int UserId, int SubscriberGroupId);
        SubscriberGroup GetSubscriberGroupWithSubscribersByID(int UserId, int SubscriberGroupId);
        void DeleteSubscriberGroupByID(int SubscriberGroupId);
        void SaveChanges();
    }
}
