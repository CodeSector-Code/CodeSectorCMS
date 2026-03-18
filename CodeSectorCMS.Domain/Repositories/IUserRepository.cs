using System;

namespace CodeSectorCMS.Domain.Repositories
{
    public interface IUserRepository : IDisposable
    {
        IEnumerable<User> GetUsers();
        User GetUserByID(int UserId);
        IEnumerable<User> FindByName(string name);
        void InsertUser(User user);
        void DeleteUser(int UserId);
        void UpdateUser(User user);
        void Save();

    }
}
