using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;
using System.Security.Claims;

namespace net_il_mio_fotoalbum.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        //Index
        public IActionResult Index(IndexFilter? data)
        {
            if (data == null) data = new IndexFilter();
            if (User.IsInRole("ADMIN"))
            {
                using (PhotoContext ctx = new PhotoContext())
                {
                    if (data.Filter == null) data.Filter = "";
                    List<Photo> photos = ctx.Photos
                        .Include(p => p.Tags)
                        .Where(p => p.Title.Contains(data.Filter))
                        .ToList();
                    data.Photos = photos;
                    return View(data);
                }

            }
            else
            {
                using (PhotoContext ctx = new PhotoContext())
                {
                    if (data.Filter == null) data.Filter = "";
                    List<Photo> photos = ctx.Photos
                        .Include(p => p.Tags)
                        .Where(p => p.Title.Contains(data.Filter))
                        .Where(p => p.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value)
                        .ToList();
                    data.Photos = photos;
                    return View(data);
                }

            }
        }

        //Details
        public IActionResult Details(int id)
        {
            using(PhotoContext ctx = new PhotoContext())
            {
                Photo photo = ctx.Photos.Include(p => p.Tags).Where(photo => photo.Id == id).FirstOrDefault();
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
                var errors = ModelState.Values.SelectMany(x => x.Errors);
                using(PhotoContext ctx = new PhotoContext())
                {
                    data.Tags = ctx.Tags.ToList();
                }
                return View("Create", data);
            }

            data.SetImageFileFromFormFile();


            using (PhotoContext ctx = new PhotoContext())
            {
                if (data.SelectedTags != null || data.SelectedTags.Count() != 0)
                {
                    foreach (int id in data.SelectedTags)
                    {
                        Tag tag = ctx.Tags.Where(t => t.Id == id).FirstOrDefault();
                        data.Photo.Tags.Add(tag);
                    }

                }
                data.Photo.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                ctx.Photos.Add(data.Photo);
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
                if(form.Photo.UserId != User.FindFirst(ClaimTypes.NameIdentifier).Value)
                {
                    return NotFound();
                }
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

                form.SetImageFileFromFormFile();

                record.Title = form.Photo.Title;
                record.Description = form.Photo.Description;
                record.Visibility = form.Photo.Visibility;
                record.Image = form.Photo.Image;
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
                if(record == null || record.UserId != User.FindFirst(ClaimTypes.NameIdentifier).Value) return NotFound();

                ctx.Photos.Remove(record);
                ctx.SaveChanges();
                
                return RedirectToAction("Index");
            }
        }

        public IActionResult Message()
        {
            using(PhotoContext ctx = new PhotoContext())
            {
                Message[] messages = ctx.Messages.Where(m => m.RecipientId == User.FindFirst(ClaimTypes.NameIdentifier).Value).ToArray();
                return View(messages);
            }
        }

        public IActionResult DeleteMessage(int id)
        {
            using (PhotoContext ctx = new PhotoContext())
            {
                Message record = ctx.Messages.Where(m => m.Id == id).FirstOrDefault();
                if (record == null) return NotFound();

                ctx.Messages.Remove(record);
                ctx.SaveChanges();

                return RedirectToAction("Message");
            }
        }

        [HttpGet]
        public IActionResult Tags()
        {
            Tag data = new Tag();
            return View(data);
        }

        [HttpPost]
		public IActionResult Tags(Tag data)
		{
            if (!ModelState.IsValid)
            {
                return View(data);
            }

            using(PhotoContext ctx = new PhotoContext())
            {
                ctx.Tags.Add(data);
                ctx.SaveChanges();
            }

			return Redirect("Index");
		}

        public IActionResult Moderate(int id)
        {
            using(PhotoContext ctx = new PhotoContext())
            {
                Photo record = ctx.Photos.Where(r => r.Id == id).FirstOrDefault();
                record.Moderate = !record.Moderate;
                ctx.SaveChanges();
            }
            return Redirect("/Admin");
        }
	}
}
