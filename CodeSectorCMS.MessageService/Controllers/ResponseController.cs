using System;
using Microsoft.AspNetCore.Mvc;
using CodeSectorCMS.Domain.Managers.Interfaces;

namespace CodeSectorCMS.MessageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        private readonly IMessageManager messageManager;

        public ResponseController(IMessageManager messageManager)
        {
            this.messageManager = messageManager;
        }

        [HttpGet("track")]
        public ActionResult Track([FromQuery] int id)
        {
            var msg = messageManager.GetMessageByID(id);
            msg.Body += "Success!";

            messageManager.SaveMessage(msg);
            messageManager.SaveChanges();

            // Return a transparent 1x1 GIF image
            var pixel = Convert.FromBase64String("R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7");
            return File(pixel, "image/gif");
        }
    }
}
