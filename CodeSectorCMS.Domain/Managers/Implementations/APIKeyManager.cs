using System;
using CodeSectorCMS.Domain.Repositories;
using CodeSectorCMS.Domain.Managers.Interfaces;

namespace CodeSectorCMS.Domain.Managers.Implementations
{
    public class APIKeyManager : IAPIKeyManager
    {
        private UnityOfWork unityOfWork;
        public APIKeyManager(UnityOfWork unityOfWork)
        {
            this.unityOfWork = unityOfWork;
        }

        public List<APIKey> GetAllAPIKeys(int ClientId)
        {
            var apiKeys = unityOfWork.APIKeyRepository.Get(includeProperties: "Client").Where(a => a.ClientID == ClientId).ToList();
            return apiKeys;   
        }
        public APIKey GetAPIKeyByID(int ClientId, int id)
        {
            var apiKey = unityOfWork.APIKeyRepository.Get().Where(a => a.ClientID == ClientId).Where(a => a.ApiKeyID == id);
            return apiKey.First();
        }
        //public void CreateNewAPIKey(APIKey apiKey)
        //{
        //    throw new NotImplementedException();
        //}
        public void SaveAPIKey(APIKey apiKey)
        {
            unityOfWork.APIKeyRepository.Insert(apiKey);
            
        }
        public void RemoveAPIKeyByID(int ClientId,int ApiKeyId)
        {
            var apiKeyToDelete = unityOfWork.APIKeyRepository.Get().Where(c => c.ClientID == ClientId).Where(t => t.ApiKeyID == ApiKeyId).First();
            unityOfWork.APIKeyRepository.Delete(apiKeyToDelete.ApiKeyID);
        }
        public void SaveChanges()
        {
            unityOfWork.Save();
        }
        public void Update(APIKey ak)
        {
            unityOfWork.APIKeyRepository.Update(ak);
        }

        public void Dispose()
        {
            unityOfWork.Dispose();
        }
            
    }
}
