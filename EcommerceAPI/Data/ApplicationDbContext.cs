using EcommerceAPI.Model.Entites;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Data
{
    public class EcommerceDbContext:DbContext
    {
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext>options):base(options)
        {

        }
        public DbSet<Product>Products { get; set; } 
        public DbSet<Category>Categories { get; set; } 
        public DbSet<ProductCategory>ProductCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>()
                .HasKey(pc => new {pc.ProductId, pc.CategoryId});
            //base.OnModelCreating(modelBuilder);
        }
    }
}
