using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;

DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
optionsBuilder.UseSqlite("Data Source=./../../../SpengerShop_Test.db");

SpengerShopContext db = new SpengerShopContext(optionsBuilder.Options);
db.Database.EnsureDeleted();
db.Database.EnsureCreated();
db.Seed();

// Alle Customer deren Vorname mit t'M' beginnt
List<Customer> result01 = db.Customers
    .Where(c => c.FirstName.StartsWith("A")).ToList();

// Gib den Customer mit der ID 12 zurück
var result02 = db.Customers
    .SingleOrDefault(c => c.Id == 12);

// Gib alle Customers zurück, die noch kein LastChangeDate haben.
var result03 = db.Customers
    .Count(c => !c.LastChangeDate.HasValue);

// Gib alle Customers zurück, deren Vorname mit 'H' beginnt und die nach '01.01.2000' geboren wurden.
var result04 = db.Customers.Where(c => 
    c.FirstName.StartsWith("H") && c.BirthDate > new DateTime(2000, 01, 01));

// Liste alle Products auf, mit dem PriceType ShortName='Normal'
var result05 = db.Products
    .Where(p => p.Prices
        .Any(p => p.CatPriceTypeNavigation.ShortName == "Normal"));

// Liste aller Customers, die das Produckt 'Awesome Cotton Shoes' gekauft haben.
var result06 = db.Customers
    .Include(c => c.ShoppingCarts.Where(s => s.CustomerNavigationId == c.Id)
        .Where(s2 => s2.ShoppingCartItems
            .Any(i => i.ProductNavigation.Name == "Awesome Cotton Shoes")));

var result101 = db.Set<Product>().AsEnumerable();

// Do Something

if (true)
{
    result101 = result101.Where(p => p.Prices
            .Any(p => p.CatPriceTypeNavigation.ShortName == "Normal"));
}

// Do Something

if (true)
{
    result101 = result101.OrderBy(p => p.Name);
}

// Do Something


var fionalResult = result101.Select(p => new { p.Name, Ean = p.Ean, SecondName = p.Name });

var result07 = db.ShoppingCarts.Where(s => s.ShoppingCartItems.Any(i => i.ProductNavigation.Name == "Awesome Cotton Shoes"));

var result100 = new { Id = 1, Name = "Martin Schrutek" };

Console.Read();
