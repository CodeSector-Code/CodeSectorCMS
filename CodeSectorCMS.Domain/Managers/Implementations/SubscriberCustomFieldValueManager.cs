using System;
using CodeSectorCMS.Domain.Repositories;
using CodeSectorCMS.Domain.Managers.Interfaces;

namespace CodeSectorCMS.Domain.Managers.Implementations
{
    public class SubscriberCustomFieldValueManager : ISubscriberCustomFieldValueManager
    {
        private readonly UnityOfWork unitOfWork;

        public SubscriberCustomFieldValueManager(UnityOfWork unityOfWork)
        {
            unitOfWork = unityOfWork;
        }
        public List<SubscriberCustomFieldValue> GetAllSubscriberCustomFieldValue(int SubscriberId)
        {
            var subCustomFieldValues = unitOfWork.SubscriberCustomFieldValueRepository.Get().Where(a => a.SubscriberCustomFieldValueID == SubscriberId).ToList();
            return subCustomFieldValues;
        }



        public void CreateNewSubscriberCustomFieldValue(SubscriberCustomFieldValue SubscriberCustomFieldValue)
        {
            unitOfWork.SubscriberCustomFieldValueRepository.Insert(SubscriberCustomFieldValue);
        }

        public SubscriberCustomFieldValue GetSubscriberCustomFieldValueByID(int SubscriberId)
        {
            return unitOfWork.SubscriberCustomFieldValueRepository.GetByID(SubscriberId);

        }


        public void DeleteSubscriberCustomFieldValueByID(int SubscriberId)
        {
            var subscriberCustomFieldValueToDelete = unitOfWork.SubscriberCustomFieldValueRepository.GetByID(SubscriberId);
            unitOfWork.SubscriberCustomFieldValueRepository.Delete(subscriberCustomFieldValueToDelete);

        }

        public void UpdateSubscriberCustomFieldValue(SubscriberCustomFieldValue SubscriberCustomFieldValue)
        {
            unitOfWork.SubscriberCustomFieldValueRepository.Update(SubscriberCustomFieldValue);
        }


        public void SaveChanges()
        {
            unitOfWork.Save();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

    }
}
