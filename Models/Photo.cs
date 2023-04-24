using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace net_il_mio_fotoalbum.Models
{
    [Table("photos")]
    public class Photo
    {
        //attributes
        [Column("id")]
        public int Id { get; set; }

        [Column("title")]
        [Required(ErrorMessage = "É necessario fornire un titolo.")]
        public string Title { get; set; }

        [Column("description")]

		public string? Description { get; set; }

        [Column("url")]
		[Required(ErrorMessage = "É necessario fornire un url.")]

		public string Url { get; set; }

        [Column("visibility")]
        public bool Visibility { get; set; }

        public List<Tag>? Tags { get; set; } = new List<Tag>();
    }
}
