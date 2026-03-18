using System;
using CodeSectorCMS.Domain.Repositories;
using CodeSectorCMS.Domain.Managers.Interfaces;

namespace CodeSectorCMS.Domain.Managers.Implementations
{
    public class SubscriberGroupManager : ISubscriberGroupManager
    {
        private UnityOfWork unitOfWork;

        public SubscriberGroupManager(UnityOfWork unityOfWork)
        {
            unitOfWork = unityOfWork;
        }
        public List<SubscriberGroup> GetAllSubscriberGroups(int UserId)
        {
            var subscriberGroup = unitOfWork.SubscriberGroupRepository.Get().Where(s => s.UserId == UserId).ToList();
            return subscriberGroup.ToList();
        }


       public void CreateNewSubscriberGroup(SubscriberGroup subscriberGroup)
        {
            unitOfWork.SubscriberGroupRepository.Insert(subscriberGroup);
        }


        public SubscriberGroup GetSubscriberGroupByID(int UserId, int SubscriberGroupId)
        {
            var subscriberGroup = unitOfWork.SubscriberGroupRepository.Get().Where(s => s.UserId == UserId).Where(s => s.SubscriberGroupID == SubscriberGroupId).First();
            return (SubscriberGroup)subscriberGroup;
        }

        public SubscriberGroup GetSubscriberGroupWithSubscribersByID(int UserId, int SubscriberGroupId)
        {
            var subscriberGroup = unitOfWork.SubscriberGroupRepository.Get(includeProperties: "Subscribers").Where(s => s.UserId == UserId).Where(s => s.SubscriberGroupID == SubscriberGroupId).First();
            return (SubscriberGroup)subscriberGroup;
        }

        public void DeleteSubscriberGroupByID(int SubscriberGroupId)
        {
            var subscriberGroupToDelete = unitOfWork.SubscriberGroupRepository.GetByID(SubscriberGroupId);
            unitOfWork.SubscriberGroupRepository.Delete(subscriberGroupToDelete);
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
