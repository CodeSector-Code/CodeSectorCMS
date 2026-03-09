using System;

namespace CodeSectorCMS.Domain.Repositories
{
    public interface ISubscriberGroupRepository : IDisposable
    {
        IEnumerable<SubscriberGroup> GetSubscriberGroups();
        SubscriberGroup GetSubscriberGroupByID(int subscriberGroupID);
        void InsertSubscriberGroup(SubscriberGroup subscriberGroup);
        void DeleteSubscriberGroup(int subscriberGroupID);
        void UpdateSubscriberGroup(SubscriberGroup subscriberGroup);
        void Save();
    }
}
