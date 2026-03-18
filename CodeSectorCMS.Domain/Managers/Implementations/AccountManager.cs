using System;
using CodeSectorCMS.Domain.Repositories;
using CodeSectorCMS.Domain.Managers.Interfaces;

namespace CodeSectorCMS.Domain.Managers.Implementations
{
    public class AccountManager : IAccountManager
    {
        private readonly UnityOfWork unitOfWork;
        public AccountManager(UnityOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public List<Account> GetAllAccounts(int UserId)
        {

            var accounts = unitOfWork.AccountRepository.Get(includeProperties: "User").Where(a => a.UserId == UserId).ToList();
            return accounts.ToList();
        }

        public void CreateNewAccount(Account account)
        {
            unitOfWork.AccountRepository.Insert(account);
            
        }

        public Account GetAccountByID(int AccountID)
        {
            return unitOfWork.AccountRepository.GetByID(AccountID);
        }

        public Account GetAccount(int AccountId, int UserId)
        {
            var accountToShow = unitOfWork.AccountRepository.Get().Where(a => a.UserId == UserId).Where(a => a.AccountID == AccountId).First();
            return accountToShow;

        }

        public void SaveAccount(Account account)
        {
            unitOfWork.AccountRepository.Update(account);
        }

        public void DeleteAccountByID(int AccountID)
        {
            var accountToDelete = unitOfWork.AccountRepository.GetByID(AccountID);
            unitOfWork.AccountRepository.Delete(accountToDelete);
        }

        public void DeleteAccount(int AccountId, int UserId)
        {
            var accountToDelete = unitOfWork.AccountRepository.Get().Where(a => a.UserId == UserId).Where(a => a.AccountID == AccountId).First();
            unitOfWork.AccountRepository.Delete(accountToDelete);
            
       
        }

        public void Details(int AccountId, int UserId)
        {
            var accountToShow = unitOfWork.AccountRepository.Get().Where(a => a.UserId == UserId).Where(a => a.AccountID == AccountId).First();
           
            
        }
        


        public void SaveChanges()
        {
            unitOfWork.Save();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

    }
}
