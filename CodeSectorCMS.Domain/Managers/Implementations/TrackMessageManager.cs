using System;
using CodeSectorCMS.Domain.Repositories;
using CodeSectorCMS.Domain.Managers.Interfaces;

namespace CodeSectorCMS.Domain.Managers.Implementations
{
    public class TrackMessageManager : ITrackMessageManager
    {
        private readonly UnityOfWork unityOfWork;

        public TrackMessageManager(UnityOfWork unityOfWork)
        {
            this.unityOfWork = unityOfWork;
        }

        public void CreateNewTrackMessage(TrackMessage trackMessage)
        {
            unityOfWork.TrackMessageRepository.Insert(trackMessage);
        }

        public List<TrackMessage> GetAllTrackedMessages()
        {
            return unityOfWork.TrackMessageRepository.GetAll().ToList();
        }

        public TrackMessage GetTrackedMessageByMessageId(int messageId)
        {
            return unityOfWork.TrackMessageRepository.Get(includeProperties: "Message").Where(x => x.MessageId == messageId).First();
        }

        public void SaveTrackMessage(TrackMessage trackMessage)
        {
            unityOfWork.TrackMessageRepository.Update(trackMessage);
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
