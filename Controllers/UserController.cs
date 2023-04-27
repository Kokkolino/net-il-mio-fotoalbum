using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Web.Helpers;

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

        [Authorize]
        public IActionResult Message()
        {

            return View();
        }
    }
}
