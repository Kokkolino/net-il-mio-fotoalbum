using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;
using System.Security.Principal;

namespace net_il_mio_fotoalbum.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly PhotoContext _ctx;

        public PhotoController(PhotoContext ctx)
        {
            _ctx = ctx;
        }

        internal bool AuthCheck()
        {
            return User.Identity.IsAuthenticated;
        }

        [HttpGet]
        public IActionResult GetPhotos([FromQuery] string? title)
        {
            if (!AuthCheck())
            {
                return Unauthorized();
            }
            var photos = _ctx.Photos.Include(p => p.Tags)
                .Where(p => title == null || p.Title.ToLower().Contains(title.ToLower()))
                .Where(p => p.Visibility)
                .Where(p => !p.Moderate)
                .ToList();
            foreach(var photo in photos)
            {
                foreach(var tag in photo.Tags)
                {
                    tag.Photos = null;
                }
            }
            return Ok(photos);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (!AuthCheck())
            {
                return Unauthorized();
            }


            var photo = _ctx.Photos
                .Include(p => p.Tags)
                .Where(p => p.Id == id)
                .Where(p => !p.Moderate)
                .FirstOrDefault();
            if (photo is null) return NotFound();

            foreach (var tag in photo.Tags) tag.Photos = null;

            return Ok(photo);


        }
    }
}
