using System.ComponentModel.DataAnnotations.Schema;

namespace net_il_mio_fotoalbum.Models
{
	[Table("messages")]
	public class Message
	{
		[Column("id")]
		public int Id { get; set; }

		[Column("name")]
		public string Name {get; set;}

		[Column("surname")]
		public string Surname { get; set;}

		[Column("email")]
		public string Email { get; set;}

		[Column("text")]
		public string Text { get; set;}
	}
}
