using System;

namespace CodeSectorCMS.Domain.Managers.Interfaces
{
   public interface ICustomFieldsManager : IDisposable
    {
        List<CustomField> GetAllCustomFields(int UserId);
        void CreateNewCustomField(CustomField customField);
        CustomField GetCustomFieldByID(int UserId, int CustomFieldID);
        void UpdateCustomField(CustomField customField); 
        void DeleteCustomFieldByID(int UserId, int CustomFieldID);
        void SaveChanges();
    }
}