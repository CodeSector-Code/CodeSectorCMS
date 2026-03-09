using System;

namespace CodeSectorCMS.Domain.Repositories
{
    public interface ICustomFieldRepository : IDisposable
    {
        IEnumerable<CustomField> GetAllCustomFields();
        CustomField GetCustomFieldByID(int CustomFieldID);
        IEnumerable<CustomField> FindByName(string name);
        void InsertCustomField(CustomField customfield);
        void DeleteCustomField(int CustomFieldID);
        void UpdateCustomField(CustomField customfield);
        void Save();
    }
}
