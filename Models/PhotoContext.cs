using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace net_il_mio_fotoalbum.Models
{
    public class PhotoContext : IdentityDbContext<IdentityUser>
    {

        public DbSet<Photo> Photos { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Message> Messages { get; set; }

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

            if (!Tags.Any())
            {
                Tag[] records =
                {
                    new Tag()
                    {
                        Name = "Urban",
                    },
                    new Tag()
                    {
                        Name = "Nature",
                    },
                    new Tag()
                    {
                        Name = "Street",
                    },
                    new Tag()
                    {
                        Name = "Art",
                    }
                };

                Tags.AddRange(records);
                SaveChanges();
			}

            if (!Roles.Any())
            {
                var seed = new IdentityRole[]
                {
                    new ("ADMIN"),
                    new ("USER")
                };

                Roles.AddRange(seed);
                SaveChanges();
            }

            if (!Users.Any(u => u.Email == "admin@admin.com")
                && !UserRoles.Any())
            {
                var admin = Users.First(u => u.Email == "admin@admin.com");

                var adminRole = Roles.First(r => r.Name == "ADMIN");

                var seed = new IdentityUserRole<string>[]
                {
                    new()
                    {
                        UserId = admin.Id,
                        RoleId = adminRole.Id
                    }
                };

                UserRoles.AddRange(seed);
            }

            SaveChanges();
        }
    }
    
}
