using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Areas.User.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Carousel> Carousels { get; set; }
        public DbSet<LastSelectedCategory> LastSelectedCategories { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(
                "Server=db29991.public.databaseasp.net; Database=db29991; User Id=db29991; Password=e!8K2R@j-oH6; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                        .Property(p => p.Price)
                        .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Mobiles" },
                new Category { Id = 2, Name = "Tablets" },
                new Category { Id = 3, Name = "Laptops"},
                new Category { Id = 4, Name = "Accessories" },
                new Category { Id = 5, Name = "TV" }
            );



        }

        internal object Find(int id)
        {
            throw new NotImplementedException();
        }
    }
}
