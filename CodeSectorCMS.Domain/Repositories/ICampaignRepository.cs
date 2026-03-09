using System;

namespace CodeSectorCMS.Domain.Repositories
{
    public interface ICampaignRepository : IDisposable
    {
        IEnumerable<Campaign> GetCampaigns();
        APIKey GetCampaignByID(int campaignID);
        void InsertCampaign(Campaign campaign);
        void DeleteCampaign(int campaignID);
        void UpdateCampaign(Campaign campaign);
        void Save();
    }
}
