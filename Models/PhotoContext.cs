using Microsoft.EntityFrameworkCore;

namespace net_il_mio_fotoalbum.Models
{
    public class PhotoContext : DbContext
    {

        public DbSet<Photo> Photos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=photo-db;Integrated Security=True;Pooling=False; TrustServerCertificate=True");
        }

        public void Seed()
        {
            if (!Photos.Any())
            {
                Photo[] records =
                {
                    new Photo()
                    {
                        Title = "photo 1",
                        Description = "Lorem ipsum",
                        Url = "example",
                        Visibility = true
                    },
                    new Photo()
                    {
                        Title = "photo 2",
                        Description = "Lorem ipsum",
                        Url = "example",
                        Visibility = true
                    },
                    new Photo()
                    {
                        Title = "photo 3",
                        Description = "Lorem ipsum",
                        Url = "example",
                        Visibility = true
                    }
                };
                Photos.AddRange(records);
                SaveChanges();
            }
        }
    }
}
