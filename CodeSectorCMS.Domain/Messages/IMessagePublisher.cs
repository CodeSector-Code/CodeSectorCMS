using System;
using CodeSectorCMS.Domain.MessageModels;

namespace CodeSectorCMS.Domain.Messages
{
    public interface IMessagePublisher
    {
        public Task Publish(CreatedMessage message);
    }
}
