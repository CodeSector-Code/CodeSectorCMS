using System;

namespace CodeSectorCMS.Domain.Managers.Interfaces
{
    public interface IAPIKeyManager : IDisposable
    {
        List<APIKey> GetAllAPIKeys(int ClientId);
        APIKey GetAPIKeyByID(int clientid, int id);
        //void CreateNewAPIKey(APIKey apiKey);
        void SaveAPIKey(APIKey apiKey);
        void RemoveAPIKeyByID(int client, int id);
        void SaveChanges();
        void Update(APIKey ak);
    }
}
