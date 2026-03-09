using System;

namespace CodeSectorCMS.Domain.Repositories
{
    public interface IAccountRepository : IDisposable
    {
        IEnumerable<Account> GetAllAccounts();
        IEnumerable<Account> FindByUsername(string username);
        Account GetAccountByID(int AccountID);
        void InsertAccount(Account account);
        void DeleteAccount(int AccountID);
        void UpdateAccount(Account account);
        void Save();
    }
}
