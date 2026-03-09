using System;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace CodeSectorCMS.Domain.Repositories
{
    public class AccountRepository : IAccountRepository, IDisposable
    {
        private CmsContext context;

        public AccountRepository(CmsContext context)
        {
            this.context = context;
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return context.Accounts.ToList();
        }

        public Account GetAccountByID(int AccountID)
        {
            return context.Accounts.Find(AccountID);
        }

        public void InsertAccount(Account account)
        {
            context.Accounts.Add(account);
        }

        public void DeleteAccount(int AccountID)
        {
            Account accountToDelete = context.Accounts.Find(AccountID);
            context.Accounts.Remove(accountToDelete);
        }

        public void UpdateAccount(Account account)
        {
            context.Entry(account).State = EntityState.Modified;
        }

        public IEnumerable<Account> FindByUsername(string username)
        {
            return context.Accounts.Where(c => c.Username.StartsWith(username)).ToList();
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
