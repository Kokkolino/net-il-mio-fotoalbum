using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Models;

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


        [HttpPost]
        public IActionResult Message(Message message)
        {
            _ctx.Messages.Add(message);
            _ctx.SaveChanges();
            return Ok();
        }
    }
}
