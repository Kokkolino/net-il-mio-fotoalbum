using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly PhotoContext _ctx;

        public UserController(PhotoContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult GetPhotos([FromQuery] string? title)
        {
            var photos = _ctx.Photos
                .Where(p => title == null || p.Title.ToLower().Contains(title.ToLower()))
                .ToList();

            return Ok(photos);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            IQueryable<Photo> photo = _ctx.Photos.Where(p => p.Id == id);

            if (photo is null) return NotFound();

            return Ok(photo);
        }
    }
}
