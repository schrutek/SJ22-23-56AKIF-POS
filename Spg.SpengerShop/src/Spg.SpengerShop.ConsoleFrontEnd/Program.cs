// Service aufrufen:

using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Application.Products;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using Spg.SpengerShop.Repository.Products;

DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
optionsBuilder.UseSqlite("Data Source=./../../../SpengerShop.db");

SpengerShopContext db = new SpengerShopContext(optionsBuilder.Options);
db.Database.EnsureDeleted();
db.Database.EnsureCreated();
db.Seed();

DbContextOptionsBuilder optionsBuilder2 = new DbContextOptionsBuilder();
optionsBuilder2.UseSqlite("Data Source=./../../../SpengerShop_Test.db");

SpengerShopContext db2 = new SpengerShopContext(optionsBuilder.Options);
db2.Database.EnsureDeleted();
db2.Database.EnsureCreated();
db2.Seed(); 

IEnumerable<Product> result = new ProductService(new TestProductRepository(db)).GetAll();
foreach (Product p in result)
{
    Console.WriteLine($"{p.Name} - {p.Ean} - {p.Material}");
}