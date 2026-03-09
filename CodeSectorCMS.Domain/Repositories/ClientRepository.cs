using System;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace CodeSectorCMS.Domain.Repositories
{
    public class ClientRepository : IClientRepository, IDisposable
    {
        private CmsContext context;

        public ClientRepository(CmsContext context)
        {
            this.context = context;
        }

        public IEnumerable<Client> GetClients()
        {
            return context.Clients.ToList();
        }

        public void InsertClient(Client client)
        {
            context.Clients.Add(client);
        }

        public void DeleteClient(int ClientID)
        {
            Client client = context.Clients.Find(ClientID);
            context.Clients.Remove(client);
        }

        public void UpdateClient(Client client)
        {
            context.Entry(client).State = EntityState.Modified;
        }

        public IEnumerable<Client> FindByName(string name)
        {
            return context.Clients.Where(c => c.Name.StartsWith(name)).ToList();
        }

        public Client GetClientByID(int ClientID)
        {
            return context.Clients.Find(ClientID);
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
