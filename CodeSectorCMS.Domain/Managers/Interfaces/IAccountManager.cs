using System;

namespace CodeSectorCMS.Domain.Managers.Interfaces
{
    public interface IAccountManager 
    {
        List<Account> GetAllAccounts(int ClientID);
        void CreateNewAccount(Account account);
        Account GetAccountByID(int AccountID);
        void DeleteAccountByID(int AccountID);
        void DeleteAccount(int AccountID, int ClientID);
        void Details(int AccountID, int ClientID);
        void SaveAccount(Account account);
        void SaveChanges();
        void Dispose();

    }
}
