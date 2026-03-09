using System;

namespace CodeSectorCMS.Domain.Repositories
{
    public interface IClientRepository : IDisposable
    {
        IEnumerable<Client> GetClients();
        Client GetClientByID(int ClientID);
        IEnumerable<Client> FindByName(string name);
        void InsertClient(Client client);
        void DeleteClient(int ClientID);
        void UpdateClient(Client client);
        void Save();

    }
}
