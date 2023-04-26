using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;

namespace net_il_mio_fotoalbum.Models
{
    public class PhotoFormModel
    {
        public Photo Photo { get; set; }

        public List<Tag>? Tags { get; set; } = new List<Tag>();

        [BindProperty]
        public List<int>? SelectedTags { get; set; } = new List<int>();

		//image
        public IFormFile? ImageFormFile { get; set; }
		public void SetImageFileFromFormFile()
		{
			if (ImageFormFile is null) return;

			using var stream = new MemoryStream();
			ImageFormFile!.CopyTo(stream);
			Photo.Image = stream.ToArray();
		}
	}
}
