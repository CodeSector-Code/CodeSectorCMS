using System;
using CodeSectorCMS.Domain.Managers.Interfaces;

using CodeSectorCMS.Domain.Repositories;

namespace CodeSectorCMS.Domain.Managers.Implementations
{
    public class ClientManager : IClientManager
    {
        private UnityOfWork unityOfWork;

        public ClientManager(UnityOfWork unityOfWork)
        {
            this.unityOfWork = unityOfWork;
        }

        public List<Client> GetAllClients()
        {
            return unityOfWork.ClientRepository.GetAll().ToList();
        }

        public Client GetClientByID(int id)
        {
            return unityOfWork.ClientRepository.GetByID(id);
        }

        public Client GetClientForTemplateByID(int id)
        {
            return unityOfWork.ClientRepository.Get(includeProperties: "SubscriberGroups,MailConfigs,Templates").Where(x => x.ClientID == id).First();
        }

        public void CreateNewClient(Client client)
        {
            unityOfWork.ClientRepository.Insert(client);
        }

        public void SaveClient(Client client)
        {
            unityOfWork.ClientRepository.Update(client);
        }

        public void RemoveClientByID(int id)
        {
            unityOfWork.ClientRepository.Delete(id);
        }

        public void SaveChanges()
        {
            unityOfWork.Save();
        }

        public void Dispose()
        {
            unityOfWork.Dispose();
        }
    }
}