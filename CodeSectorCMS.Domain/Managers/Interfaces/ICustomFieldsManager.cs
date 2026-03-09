using System;

namespace CodeSectorCMS.Domain.Managers.Interfaces
{
   public interface ICustomFieldsManager : IDisposable
    {
        List<CustomField> GetAllCustomFields(int ClientId);
        void CreateNewCustomField(CustomField customField);
        CustomField GetCustomFieldByID(int ClientId, int CustomFieldID);
        void UpdateCustomField(CustomField customField); 
        void DeleteCustomFieldByID(int ClientId, int CustomFieldID);
        void SaveChanges();
    }
}