using System;
using CodeSectorCMS.Domain.Repositories;
using CodeSectorCMS.Domain.Managers.Interfaces;


namespace CodeSectorCMS.Domain.Managers.Implementations
{
    public class CustomFieldManager : ICustomFieldsManager
    {

        private UnityOfWork unitOfWork;

        public CustomFieldManager(UnityOfWork unityOfWork)
        {
            this.unitOfWork = unityOfWork;
        }

        public List<CustomField> GetAllCustomFields(int UserId)
        {
            var customField = unitOfWork.CustomFieldRepository.Get(includeProperties: "User").Where(a => a.UserId == UserId).ToList();
            return customField;
        
        }


        public void CreateNewCustomField(CustomField customField)
        {
            unitOfWork.CustomFieldRepository.Insert(customField);

        }

        public CustomField GetCustomFieldByID(int UserId, int CustomFieldId)
        {
            var customField = unitOfWork.CustomFieldRepository.Get().Where(a => a.UserId == UserId).Where(c => c.CustomFieldID == CustomFieldId).First();
            return customField;
        }
        public void UpdateCustomField(CustomField customField)
        {
            unitOfWork.CustomFieldRepository.Update(customField);
        }
        public void DeleteCustomFieldByID(int UserId, int CustomFieldId)
        {
            var customFieldToDelete = unitOfWork.CustomFieldRepository.Get().Where(c => c.UserId == UserId).Where(c => c.CustomFieldID == CustomFieldId).First();
            unitOfWork.CustomFieldRepository.Delete(customFieldToDelete.CustomFieldID);
        
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
