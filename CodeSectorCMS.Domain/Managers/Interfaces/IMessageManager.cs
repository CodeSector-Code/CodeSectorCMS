using System;

namespace CodeSectorCMS.Domain.Managers.Interfaces
{
    public interface IMessageManager : IDisposable
    {
        List<Message> GetAllMessages();
        Message GetMessageByID(int id);
        void CreateNewMessage(Message message);
        void SaveMessage(Message message);
        void RemoveMessageByID(int id);
        void SaveChanges();
    }
}
