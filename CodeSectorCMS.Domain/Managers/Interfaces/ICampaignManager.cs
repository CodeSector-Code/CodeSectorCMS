using System;

// Our namespace
using CodeSectorCMS.Domain;
using CodeSectorCMS.Domain.MessageModels;

namespace CodeSectorCMS.Domain.Managers.Interfaces
{
    public interface ICampaignManager : IDisposable
    {
        List<Campaign> GetAllCampaigns();
        Campaign GetCampaignByID(int id);
        void CreateNewCampaign(Request request);
        void SaveCampaign(Campaign campaign);
        void RemoveCampaignByID(int id);
        void SaveChanges();
    }
}
