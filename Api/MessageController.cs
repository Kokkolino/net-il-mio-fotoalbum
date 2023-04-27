using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Models;
using System.Security.Claims;

namespace net_il_mio_fotoalbum.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly PhotoContext _ctx;

        public MessageController(PhotoContext ctx)
        {
            _ctx = ctx;
        }

        internal bool AuthCheck()
        {
            return User.Identity.IsAuthenticated;
        }



        [HttpGet("comment/{id}")]
        public IActionResult Get(int id)
        {
            List<Message> messages = _ctx.Messages
                .Where(m => m.PhotoId == id)
                .ToList();

            return Ok(messages);
        }

        [HttpPost("help")]
        public IActionResult Help(Message message)
        {
            if (!AuthCheck())
            {
                return Unauthorized();
            }


            message.SenderId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            message.RecipientId = "d9b60ff9-9750-49c4-8e8c-34fb56e70163";
            message.Email = User.FindFirst(ClaimTypes.Email).Value;

            _ctx.Messages.Add(message);
            _ctx.SaveChanges();
            return Ok();
        }

        [HttpPost("comment/{id}")]
        public IActionResult Comment(int id ,Message message)
        {

            if (!AuthCheck())
            {
                return Unauthorized();
            }

            message.SenderId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            message.PhotoId = id;
            message.Email = User.FindFirst(ClaimTypes.Email).Value;

            _ctx.Messages.Add(message);
            _ctx.SaveChanges();
            return Ok();

        }
    }
}
