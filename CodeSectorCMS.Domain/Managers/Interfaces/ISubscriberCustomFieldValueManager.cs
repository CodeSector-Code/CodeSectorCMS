using System;

namespace CodeSectorCMS.Domain.Managers.Interfaces
{
    public interface ISubscriberCustomFieldValueManager
    {
        List<SubscriberCustomFieldValue> GetAllSubscriberCustomFieldValue(int SubscriberId);
        void CreateNewSubscriberCustomFieldValue(SubscriberCustomFieldValue SubscriberCustomFieldValue);
        SubscriberCustomFieldValue GetSubscriberCustomFieldValueByID(int SubscriberId);
        void DeleteSubscriberCustomFieldValueByID(int SubscriberId);
        void UpdateSubscriberCustomFieldValue(SubscriberCustomFieldValue SubscriberCustomFieldValue);
        void SaveChanges();

    }
}
