using System;

namespace CodeSectorCMS.Domain.MessageModels
{

    public enum Status { success, fail };

    public  class Response

    {
        public Status Status { get; set; }
        public string Message { get; set; }

    }
}
