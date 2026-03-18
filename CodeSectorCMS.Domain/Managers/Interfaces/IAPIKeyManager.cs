using System;

namespace CodeSectorCMS.Domain.Managers.Interfaces
{
    public interface IAPIKeyManager : IDisposable
    {
        List<APIKey> GetAllAPIKeys(int UserId);
        APIKey GetAPIKeyByID(int UserId, int id);
        //void CreateNewAPIKey(APIKey apiKey);
        void SaveAPIKey(APIKey apiKey);
        void RemoveAPIKeyByID(int UserId, int id);
        void SaveChanges();
        void Update(APIKey ak);
    }
}
