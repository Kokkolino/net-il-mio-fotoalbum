using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [HttpGet]
        public IActionResult Create()
        { 
            PhotoFormModel model = new PhotoFormModel();
            List<Tag> listTags = new List<Tag>();
			using (PhotoContext ctx = new PhotoContext())
			{
				foreach (Tag tag in ctx.Tags)
                {
                    listTags.Add(tag);
                }
            }
            model.Tags = listTags;
            model.Photo = new Photo();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PhotoFormModel data)
        {
            data.Tags = new List<Tag>();
            //Model validation
            if(!ModelState.IsValid)
            {

                var errors = ModelState.Values.SelectMany(x => x.Errors);
                using(PhotoContext ctx = new PhotoContext())
                {
                    data.Tags = ctx.Tags.ToList();
                }
                return View("Create", data);
            }

            //New record Photo

            Photo record = new Photo();
            record = data.Photo;

            if(data.SelectedTags != null || data.SelectedTags.Count() != 0)
            {
                foreach(int id in data.SelectedTags)
                {
                    using(PhotoContext ctx = new PhotoContext())
                    {
                        Tag tag = new Tag();
                        tag = ctx.Tags.Where(t => t.Id == id).First();
                        record.Tags.Add(tag);
                    }
                }
            }


            using(PhotoContext ctx = new PhotoContext())
            {
                ctx.Photos.Add(record);
                ctx.SaveChanges();
            }
            

            return Redirect("Index");
        }

    }
}
