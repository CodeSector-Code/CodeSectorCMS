using System;
using Microsoft.AspNetCore.Mvc;
using CodeSectorCMS.Domain.Managers.Interfaces;

namespace CodeSectorCMS.MessageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        private readonly ITrackMessageManager trackMessageManager;

        public ResponseController(ITrackMessageManager messageManager)
        {
            this.trackMessageManager = messageManager;
        }

        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet("track")]
        public ActionResult Track([FromQuery] int id)
        {
            var msg = trackMessageManager.GetTrackedMessageByMessageId(id);

            if (msg != null)
            {
                msg.Opened = true;
                msg.OpenCount += 1;

                trackMessageManager.SaveTrackMessage(msg);
                trackMessageManager.SaveChanges();
            }

            // Return a transparent 1x1 GIF image
            var pixel = Convert.FromBase64String("R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7");
            return File(pixel, "image/gif");
        }
    }
}
