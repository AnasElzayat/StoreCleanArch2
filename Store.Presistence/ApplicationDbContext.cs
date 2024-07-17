using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.Domain;

namespace Store.Presistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole
                {
                    Id = 1,
                    Name = "ADMIN",
                    NormalizedName = "ADMIN"
                },
                new ApplicationRole
                {
                    Id = 2,
                    Name = "USER",
                    NormalizedName = "USER"
                }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Phone" },
                new Category { Id = 2, Name = "Car" }
            );


            modelBuilder.Entity<Product>()
                .HasData(new Product() { Id = 1, Name = "Oppo", Description = "Flagship one", Price = 2000, Quantity = 10, CatId = 1 },
                new Product() { Id = 2, Name = "IPhone", Description = "Flagship one", Price = 3000, Quantity = 10, CatId = 1 },
                new Product() { Id = 3, Name = "Kia", Description = "Expinsive one", Price = 20000, Quantity = 10, CatId = 2 },
                new Product() { Id = 4, Name = "Bmw", Description = "Expinsive one", Price = 30000, Quantity = 10, CatId = 2 }
                );





        }
    }
}
