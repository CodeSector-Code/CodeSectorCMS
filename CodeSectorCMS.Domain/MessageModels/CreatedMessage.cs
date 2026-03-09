using System;

namespace CodeSectorCMS.Domain.MessageModels
{
    public class CreatedMessage
    {
        public int MessageID { get; set; }
        public string Email { get; set; }
        public bool SentFLAG { get; set; }
        public MonitoringSTATUS  MonitoringStatus { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public int MailConfigId { get; set; }
    }
}