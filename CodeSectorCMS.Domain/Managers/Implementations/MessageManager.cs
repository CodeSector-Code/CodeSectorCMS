using System;

// Our namespaces
using CodeSectorCMS.Domain.Repositories;
using CodeSectorCMS.Domain.Managers.Interfaces;

namespace CodeSectorCMS.Domain.Managers.Implementations
{
    public class MessageManager : IMessageManager
    {
        private UnityOfWork Unit;

        public MessageManager(UnityOfWork unityOfWork)
        {
            Unit = unityOfWork;
        }

        public List<Message> GetAllMessages()
        {
            var messages = Unit.MessageRepository.GetAll().ToList();

            return messages;
        }


        public Message GetMessageByID(int id)
        {
            Message message = Unit.MessageRepository.GetByID(id);

            return message;
        }


        public void CreateNewMessage(Message message)
        {
            Unit.MessageRepository.Insert(message);
        }

        public void SaveMessage(Message message)
        {
            Unit.MessageRepository.Update(message);
        }

        public void RemoveMessageByID(int id)
        {
            Unit.MessageRepository.Delete(id);
        }

        public void SaveChanges()
        {
            Unit.Save();
        }

        public void Dispose()
        {
            Unit.Dispose();
        }
    }
}
