using System;
using CodeSectorCMS.Domain.Managers.Interfaces;

using CodeSectorCMS.Domain.Repositories;

namespace CodeSectorCMS.Domain.Managers.Implementations
{
    public class UserManager : IUserManager
    {
        private UnityOfWork unityOfWork;

        public UserManager(UnityOfWork unityOfWork)
        {
            this.unityOfWork = unityOfWork;
        }

        public List<User> GetAllUsers()
        {
            return unityOfWork.UserRepository.GetAll().ToList();
        }

        public User GetUserByID(int id)
        {
            return unityOfWork.UserRepository.GetByID(id);
        }

        public User GetUserForTemplateByID(int id)
        {
            return unityOfWork.UserRepository.Get(includeProperties: "SubscriberGroups,MailConfigs,Templates").Where(x => x.UserId == id).First();
        }

        public void CreateNewUser(User user)
        {
            unityOfWork.UserRepository.Insert(user);
        }

        public void SaveUser(User user)
        {
            unityOfWork.UserRepository.Update(user);
        }

        public void RemoveUserByID(int id)
        {
            unityOfWork.UserRepository.Delete(id);
        }

        public void SaveChanges()
        {
            unityOfWork.Save();
        }

        public void Dispose()
        {
            unityOfWork.Dispose();
        }
    }
}