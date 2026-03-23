using System;

namespace CodeSectorCMS.Domain.Managers.Interfaces
{
    public interface ITrackMessageManager : IDisposable
    {
        TrackMessage GetTrackedMessageByMessageId(int messageId);
        List<TrackMessage> GetAllTrackedMessages();
        void CreateNewTrackMessage(TrackMessage trackMessage);
        void SaveTrackMessage(TrackMessage trackMessage);
        void SaveChanges();
    }
}
