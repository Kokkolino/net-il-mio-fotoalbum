using Microsoft.AspNetCore.Mvc;

namespace net_il_mio_fotoalbum.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
        public IActionResult Message()
        {
            return View();
        }
    }
}
