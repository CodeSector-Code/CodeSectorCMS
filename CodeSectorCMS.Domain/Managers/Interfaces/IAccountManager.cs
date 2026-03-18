using System;

namespace CodeSectorCMS.Domain.Managers.Interfaces
{
    public interface IAccountManager 
    {
        List<Account> GetAllAccounts(int UserId);
        void CreateNewAccount(Account account);
        Account GetAccountByID(int AccountID);
        void DeleteAccountByID(int AccountID);
        void DeleteAccount(int AccountID, int UserId);
        void Details(int AccountID, int UserId);
        void SaveAccount(Account account);
        void SaveChanges();
        void Dispose();
    }
}
