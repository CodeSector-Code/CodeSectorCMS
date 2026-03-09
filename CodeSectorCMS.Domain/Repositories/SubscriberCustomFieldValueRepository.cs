using System;
using Microsoft.EntityFrameworkCore;

namespace CodeSectorCMS.Domain.Repositories
{
    public class SubscriberCustomFieldValueRepository : ISubscriberCustomFieldValueRepository
    {
        private CmsContext context;

        public SubscriberCustomFieldValueRepository(CmsContext context)
        {
            this.context = context;
        }

        public IEnumerable<SubscriberCustomFieldValue> GetAllSubscriberCustomFieldValues()
        {
            return context.SubscriberCustomFieldValues.ToList();
        }

       public SubscriberCustomFieldValue GetSubscriberCustomFieldValueByID(int SubscriberCustomFieldValueId)
        {
            return context.SubscriberCustomFieldValues.Find(SubscriberCustomFieldValueId);
        }

       public void DeleteSubscriberCustomFieldValue(int SubscriberCustomFieldId)
       {
           SubscriberCustomFieldValue scfValue = context.SubscriberCustomFieldValues.Find(SubscriberCustomFieldId);
           context.SubscriberCustomFieldValues.Remove(scfValue);
       }

       public void InsertSubscriberCustomFieldValue(SubscriberCustomFieldValue value)
       {
           context.SubscriberCustomFieldValues.Add(value);
       }


       public void UpdateSubscriberCustomFieldValue(SubscriberCustomFieldValue value)
       {
           context.Entry(value).State = EntityState.Modified;

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
