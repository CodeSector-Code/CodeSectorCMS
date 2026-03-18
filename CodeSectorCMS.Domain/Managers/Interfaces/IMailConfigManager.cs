using System;

namespace CodeSectorCMS.Domain.Managers.Interfaces
{
    public interface IMailConfigManager : IDisposable
    {
        List<MailConfig> GetAllMailConfigs(int UserId);
        MailConfig GetMailConfigByID(int id);
        void CreateNewMailConfig(MailConfig mailconfig);
        void SaveMailConfig(MailConfig mailconfig);
        void RemoveMailConfigByID(int id);
        void SaveChanges();
    }
}
