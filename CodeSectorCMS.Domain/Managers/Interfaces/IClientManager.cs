using System;

namespace CodeSectorCMS.Domain.Managers.Interfaces
{
    public interface IClientManager : IDisposable
    {
        List<Client> GetAllClients();
        Client GetClientByID(int id);
        Client GetClientForTemplateByID(int id);
        void CreateNewClient(Client client);
        void SaveClient(Client client);
        void RemoveClientByID(int id);
        void SaveChanges();
    }
}
