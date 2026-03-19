using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeSectorCMS.MessageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        [HttpGet("track")]
        public ActionResult Track([FromQuery] string id)
        {
            // *** Your Logic Here ***
            // 1. Log the email open event to your database or logging system.
            //    Use the 'email' and 'eventId' parameters to identify the recipient and campaign.
            //Console.WriteLine($"Email opened by: {email}, Event ID: {eventId}");
            // 2. Add server-side logic to handle potential automated email client requests 
            //    versus actual user opens.

            // Return a transparent 1x1 GIF image
            var pixel = Convert.FromBase64String("R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7");
            return File(pixel, "image/gif");
        }
    }
}
