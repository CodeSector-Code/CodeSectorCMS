using System;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace CodeSectorCMS.Domain.Repositories
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private CmsContext context;

        public UserRepository(CmsContext context)
        {
            this.context = context;
        }

        public IEnumerable<User> GetUsers()
        {
            return context.Userss.ToList();
        }

        public void InsertUser(User user)
        {
            context.Userss.Add(user);
        }

        public void DeleteUser(int UserId)
        {
            User user = context.Userss.Find(UserId);
            context.Userss.Remove(user);
        }

        public void UpdateUser(User user)
        {
            context.Entry(user).State = EntityState.Modified;
        }

        public IEnumerable<User> FindByName(string name)
        {
            return context.Userss.Where(c => c.Name.StartsWith(name)).ToList();
        }

        public User GetUserByID(int UserId)
        {
            return context.Userss.Find(UserId);
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
