using System;

namespace CodeSectorCMS.Domain
{
    public enum MonitoringSTATUS { read, bounced, unknown }

    public class Message : BaseEntity
    {
        public int MessageID { get; set; }
        public int CampaignID { get; set; }
        public bool SentFLAG { get; set; }
        public string Body { get; set; }
        public virtual Campaign Campaign { get; set; }
        public MonitoringSTATUS MonitoringStatus { get; set; }
    }
}
