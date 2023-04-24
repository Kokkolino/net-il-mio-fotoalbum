using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace net_il_mio_fotoalbum.Models
{
    [Table("tags")]
    public class Tag
    {

        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        public List<Photo> Photos { get; set; } = new List<Photo>();
    }
}
