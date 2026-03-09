using System;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace CodeSectorCMS.Domain.Repositories
{
    public class CustomFieldRepository : ICustomFieldRepository, IDisposable
    {
        private CmsContext context;

        public CustomFieldRepository(CmsContext context)
        {
            this.context = context;
        }

        public IEnumerable<CustomField> GetAllCustomFields()
        {
            return context.CustomFields.ToList();
        }

        public CustomField GetCustomFieldByID(int CustomFieldID)
        {
            return context.CustomFields.Find(CustomFieldID);
        }

        public IEnumerable<CustomField> FindByName(string name)
        {
            return context.CustomFields.Where(c => c.Name.StartsWith(name)).ToList();
        }

        public void InsertCustomField(CustomField customfield)
        {
            context.CustomFields.Add(customfield);
        }


        public void UpdateCustomField(CustomField customfield)
        {
            context.Entry(customfield).State = EntityState.Modified;
        }


        public void DeleteCustomField(int CustomFieldID)
        {
            CustomField customfieldToRemove = context.CustomFields.Find(CustomFieldID);
            context.CustomFields.Remove(customfieldToRemove);
        }

        public void Save()
        {
            context.SaveChanges();
        }


        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
