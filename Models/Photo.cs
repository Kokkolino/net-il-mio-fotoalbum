using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace net_il_mio_fotoalbum.Models
{
    [Table("photos")]
    public class Photo
    {

        public Photo()
        {
            Tags = new List<Tag>();
        }
        //attributes
        [Column("id")]
        public int Id { get; set; }

        [Column("title")]
        [Required(ErrorMessage = "É necessario fornire un titolo.")]
        public string Title { get; set; }

        [Column("description")]

		public string? Description { get; set; }

        [Column("url")]
		public string Url { get; set; }

        [Column("image")]
        public byte[]? Image { get; set; }

        public string Src => Image is null 
            ? Url 
            : $"data:image/png;base64,{Convert.ToBase64String(Image)}";

		[Column("visibility")]
        public bool Visibility { get; set; }

        [Column("moderate")]
        public bool Moderate { get; set; }

        [Column("user_id")]
        public string? UserId { get; set;}

        public List<Message> Messages { get; set; }
        public List<Tag>? Tags { get; set; } = new List<Tag>();
    }
}
