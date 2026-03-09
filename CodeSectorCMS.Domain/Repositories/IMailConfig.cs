using System;

namespace CodeSectorCMS.Domain.Repositories
{
    public interface IMailConfig : IDisposable
    {
        IEnumerable<MailConfig> GetAllMailConfigs();
        MailConfig GetMailConfigByID(int MailConfigID);
        IEnumerable<MailConfig> FindByEmail(string email);
        void InsertMailConfig(MailConfig mailconfig);
        void DeleteMailConfig(int MailConfigID);
        void UpdateMailConfig(MailConfig mailconfig);
        void Save();
    }
}
