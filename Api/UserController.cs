using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly PhotoContext _ctx;

        public UserController(PhotoContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<Photo> photos = _ctx.Photos.Include(p => p.Tags);
            return Ok(photos);
        }

        [HttpGet]
        public IActionResult Get(string title)
        {
            IQueryable<Photo> photos = _ctx.Photos.Where(p => p.Title.ToLower().Contains(title.ToLower()))
                .Include(p => p.Tags);
            return Ok(photos);
        }

        public IActionResult Get(int id)
        {
            IQueryable<Photo> photo = _ctx.Photos.Where(p => p.Id == id).Include(p => p.Tags);
            return Ok(photo);
        }
    }
}
