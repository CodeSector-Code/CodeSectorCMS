using System;

namespace CodeSectorCMS.Domain.Managers.Interfaces
{
    public interface IUserManager : IDisposable
    {
        List<User> GetAllUsers();
        User GetUserByID(int id);
        User GetUserForTemplateByID(int id);
        void CreateNewUser(User User);
        void SaveUser(User User);
        void RemoveUserByID(int id);
        void SaveChanges();
    }
}
