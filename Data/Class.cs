using AutoFixture;
using Microsoft.EntityFrameworkCore;

namespace mymulticontainer.Data
{

    public class Product
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? SKU { get; set; }
        public string? Code { get; set; }
    }
    public enum ProductCategory
    {
        Clothing = 1,
        Footware = 2,
        Electronics = 3,
        Household = 4
    }

    public class SalesContext : DbContext
    {
        public SalesContext(DbContextOptions<SalesContext> options) : base(options)
        {
        }

        public DbSet<Product>? Products { get; set; }
    }

    public static class Seeder
    {

        // This is purely for a demo, don't anything like this in a real application!
        public static void Seed(this SalesContext salesContext)
        {
            if (!salesContext.Products.Any())
            {
                Fixture fixture = new Fixture();
                fixture.Customize<Product>(product => product.Without(p => p.ProductId));
                //--- The next two lines add 100 rows to your database
                List<Product> products = fixture.CreateMany<Product>(100).ToList();
                salesContext.AddRange(products);
                salesContext.SaveChanges();
            }
        }
    }
}
