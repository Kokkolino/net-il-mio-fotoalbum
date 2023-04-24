using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace net_il_mio_fotoalbum.Models
{
    public class PhotoFormModel
    {
        public Photo Photo { get; set; }

        public List<Tag>? Tags { get; set; } = new List<Tag>();

        [BindProperty]
        public List<int>? SelectedTags { get; set; } = new List<int>();


    }
}
