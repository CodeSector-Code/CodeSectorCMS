using System;

namespace CodeSectorCMS.Domain.MessageModels
{
    public class ExtendedCampaign
    {
        public Campaign Campaign { get; set; }
        public List<TrackMessage> TrackMessage { get; set; }
    }
}
