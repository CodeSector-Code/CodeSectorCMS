using System;
using CodeSectorCMS.Domain.Repositories;
using CodeSectorCMS.Domain.Managers.Interfaces;

namespace CodeSectorCMS.Domain.Managers.Implementations
{
    public class SubscriberManager :  ISubscriberManager 
    {
        private UnityOfWork unitOfWork;

        public SubscriberManager(UnityOfWork unityOfWork)
        {
            unitOfWork = unityOfWork;
        }

        public List<Subscriber> GetAllSubscribers(int UserId)
        {
            var subscribers = unitOfWork.SubscriberRepository.Get().Where(a => a.UserId == UserId).ToList();
            return subscribers;
        }

        public List<Subscriber> GetAllSubscribersWithCustomFieldValues(int UserId)
        {
            var subscribers = unitOfWork.SubscriberRepository.Get(includeProperties: "SubscriberCustomFieldValues").Where(a => a.UserId == UserId).ToList();
            return subscribers;
        }

        public void CreateNewSubscriber(Subscriber Subscriber)
        {
            unitOfWork.SubscriberRepository.Insert(Subscriber);
        
        }




        public Subscriber GetSubscriberByID(int UserId, int SubscriberId)
        {
            var subscriber = unitOfWork.SubscriberRepository.Get().Where(a => a.UserId == UserId).Where(a => a.SubscriberID == SubscriberId).First();
            return (Subscriber) subscriber;
       
        }




        public void DeleteSubscriberByID(int UserId, int SubscriberId)
         {
             var templateToDelete = unitOfWork.SubscriberRepository.Get().Where(c => c.UserId == UserId).Where(s => s.SubscriberID == SubscriberId).First();
             unitOfWork.SubscriberRepository.Delete(templateToDelete);
        
         }


        public void UpdateSubscriber(Subscriber subscriber)
        {
            unitOfWork.SubscriberRepository.Update(subscriber);
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
