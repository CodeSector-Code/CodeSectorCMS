using System;
using CodeSectorCMS.Domain;
using CodeSectorCMS.Domain.MessageModels;

namespace CodeSectorCMS.MessageService.Services
{
    public interface IMailService
    {
        void SendMail(MailConfig senderMailConfig, CreatedMessage message, string recieverAddress);
    }
}
