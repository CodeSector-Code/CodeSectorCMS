using System;


namespace CodeSectorCMS.Domain.Repositories
{
    public interface ISubscriberCustomFieldValueRepository : IDisposable
    {
        IEnumerable<SubscriberCustomFieldValue> GetAllSubscriberCustomFieldValues();
        SubscriberCustomFieldValue GetSubscriberCustomFieldValueByID(int SubscriberCustomFieldValueId);
        void InsertSubscriberCustomFieldValue(SubscriberCustomFieldValue value);
        void DeleteSubscriberCustomFieldValue(int SubscriberCustomFieldId);
        void UpdateSubscriberCustomFieldValue(SubscriberCustomFieldValue value);
        void Save();
    }
}
