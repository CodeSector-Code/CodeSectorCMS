using System;
using CodeSectorCMS.Domain.Repositories;
using CodeSectorCMS.Domain.Managers.Interfaces;


namespace CodeSectorCMS.Domain.Managers.Implementations
{
    public class MailConfigManager : IMailConfigManager
    {
        private readonly UnityOfWork unitOfWork;

        public MailConfigManager(UnityOfWork unityOfWork)
        {
            this.unitOfWork = unityOfWork;
        }
        public List<MailConfig> GetAllMailConfigs(int UserId)
        {

            var mailConfig = unitOfWork.MailConfigRepository.Get(includeProperties: "User").Where( a => a.UserId == UserId).ToList();
            return mailConfig;
        }


        public MailConfig GetMailConfigByID(int id)
        {
            return unitOfWork.MailConfigRepository.GetByID(id);
        }


        public void CreateNewMailConfig(MailConfig mailconfig)
        {
            unitOfWork.MailConfigRepository.Insert(mailconfig);
        }

        public void SaveMailConfig(MailConfig mailconfig)
        {
            unitOfWork.MailConfigRepository.Update(mailconfig);
        }

        public void RemoveMailConfigByID(int id)
        {
            unitOfWork.MailConfigRepository.Delete(id);
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
