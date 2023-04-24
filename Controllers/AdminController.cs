using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    public class AdminController : Controller
    {
        //Index
        public IActionResult Index()
        {
            using(PhotoContext ctx = new PhotoContext())
            {
                Photo[] photos = ctx.Photos.ToArray();
                return View(photos);
            }
        }
        //Details
        public IActionResult Details(int id)
        {
            using(PhotoContext ctx = new PhotoContext())
            {
                Photo photo = ctx.Photos.Where(photo => photo.Id == id).FirstOrDefault();
                return View(photo);
            }
        }
        //Create
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
                //var errors = ModelState.Values.SelectMany(x => x.Errors);
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
                using(PhotoContext ctx = new PhotoContext())
                {
                    foreach(int id in data.SelectedTags)
                    {
                        Tag tag = ctx.Tags.Where(t => t.Id == id).FirstOrDefault();
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

        //Update
        [HttpGet]
        public IActionResult Update(int id)
        {
            PhotoFormModel form = new PhotoFormModel();
            using(PhotoContext ctx = new PhotoContext())
            {
                form.Photo = ctx.Photos.Where(p => p.Id == id).Include(p => p.Tags).FirstOrDefault();

				if (form.Photo != null)
				{
                    form.Tags = ctx.Tags.ToList();  
                    
                    return View(form);
				}
                else
                {
                    return NotFound();
                }
			}

		}

        [HttpPost]
        public IActionResult Update (PhotoFormModel form, int id)
        {
            if (!ModelState.IsValid)
            {
                using(PhotoContext ctx = new PhotoContext())
                {
					form.Tags = ctx.Tags.ToList();
				}

				return View(form);
            }
            using(PhotoContext ctx = new PhotoContext())
            {
                Photo record = ctx.Photos.Where(p => p.Id == id).Include(p => p.Tags).FirstOrDefault();
                if(record == null) return NotFound();

                record.Title = form.Photo.Title;
                record.Description = form.Photo.Description;
                record.Visibility = form.Photo.Visibility;
                record.Url = form.Photo.Url;

                record.Tags.Clear();
				foreach (int tagId in form.SelectedTags)
				{
                    Tag tag = ctx.Tags.Where(t => t.Id == tagId).FirstOrDefault();
                    record.Tags.Add(tag);
			    }
                ctx.SaveChanges();
                return RedirectToAction("Index");
			}
        }

        public IActionResult Delete(int id)
        {
            using(PhotoContext ctx = new PhotoContext())
            {
                Photo record = ctx.Photos.Where(p => p.Id == id).FirstOrDefault();
                if(record == null) return NotFound();

                ctx.Photos.Remove(record);
                ctx.SaveChanges();
                
                return RedirectToAction("Index");
            }
        }
    }
}
