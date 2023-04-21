using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            using(PhotoContext ctx = new PhotoContext())
            {
                Photo[] photos = ctx.Photos.ToArray();
                return View(photos);
            }
        }

        public IActionResult Details(int id)
        {
            using(PhotoContext ctx = new PhotoContext())
            {
                Photo photo = ctx.Photos.Where(photo => photo.Id == id).FirstOrDefault();
                return View(photo);
            }
        }
    }
}
