using System;

namespace CodeSectorCMS.Domain
{
    public class TrackMessage : BaseEntity
    {
        public int Id { get; set; }
        public int MessageId { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Receiver { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public bool Sent { get; set; }
        public bool Opened { get; set; }
        public int OpenCount { get; set; }
        public string ClickedOn { get; set; } = string.Empty;
        public string Response { get; set; } = string.Empty;
        public int Status { get; set; }
    }
}
