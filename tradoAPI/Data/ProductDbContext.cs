using Microsoft.EntityFrameworkCore;
using tradoAPI.Models;

namespace tradoAPI.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var decimalProps = modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => (System.Nullable.GetUnderlyingType(p.ClrType) ?? p.ClrType) == typeof(decimal));

            foreach (var property in decimalProps)
            {
                property.SetPrecision(18);
                property.SetScale(2);
            }
        }
        //Dbset
        public DbSet<Products> Products { get; set; }

        public DbSet<Carts> Carts { get; set; }

        public DbSet<Orders> Orders { get; set; } 

    }

}
