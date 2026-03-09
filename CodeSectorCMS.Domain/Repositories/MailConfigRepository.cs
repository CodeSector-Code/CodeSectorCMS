using System;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace CodeSectorCMS.Domain.Repositories
{
   public  class MailConfigRepository : IMailConfig, IDisposable
    {
       private CmsContext context;

       public MailConfigRepository(CmsContext context)
       {
           this.context = context;
       }

       public IEnumerable<MailConfig> GetAllMailConfigs()
       {
           return context.MailConfigs.ToList();
       }


       public MailConfig GetMailConfigByID(int MailConfigID)
       {
           return context.MailConfigs.Find(MailConfigID);
       }

       public IEnumerable<MailConfig> FindByEmail(string email)
       {
           return context.MailConfigs.Where(m => m.Email.StartsWith(email)).ToList();
       }

       public void InsertMailConfig(MailConfig mailconfig)
       {
           context.MailConfigs.Add(mailconfig);
       }


       public void DeleteMailConfig(int MailConfigID)
       {
           MailConfig mailconfigToRemove = context.MailConfigs.Find(MailConfigID);
           context.MailConfigs.Remove(mailconfigToRemove);
       }

       public void UpdateMailConfig(MailConfig mailconfig)
       {
           context.Entry(mailconfig).State = EntityState.Modified;
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
