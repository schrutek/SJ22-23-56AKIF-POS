using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Spg.SpengerShop.Infrastructure
{
    // 1. Diese Klasse muss von DBContext ableiten
    public class SpengerShopContext : DbContext
    {
        // 2. Die Tabellen der DB als Properties auflisten
        public DbSet<Shop> Shops => Set<Shop>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Price> Prices => Set<Price>();
        public DbSet<CatPriceType> CatPriceTypes => Set<CatPriceType>();
        public DbSet<ShoppingCart> ShoppingCarts => Set<ShoppingCart>();
        public DbSet<ShoppingCartItem> ShoppingCartItems => Set<ShoppingCartItem>();

        // 3. Constructor
        public SpengerShopContext()
        { }

        public SpengerShopContext(DbContextOptions options)
            : base (options)
        { }

        // 4. Konfiguration vor DB Erstellung
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);

            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseSqlite("Data Source = Server=142.147.258.169;Database=myDataBase;User Id=Ich;Password=Geheim!");
            //}
        }

        // 5. Optionen während DB Erstellung
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Product>().ToTable("Produkte"); Wollen wir nicht ändern

            modelBuilder.Entity<Product>().HasKey(p => p.Name); // OK
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired(); // OK

            // Nice, aber unnötig ("Convention over Configuration")
            //modelBuilder.Entity<Product>().HasMany(p => p.ShoppingCartItems);
            //modelBuilder.Entity<Customer>().HasMany(p => p.ShoppingCarts);

            // Value Object:
            modelBuilder.Entity<Customer>().OwnsOne(c => c.Address);
            modelBuilder.Entity<Shop>().OwnsOne(c => c.Address);
        }

        public void Seed()
        {
            Randomizer.Seed = new Random(181025);

            List<CatPriceType> catPriceTypes = new List<CatPriceType>()
            {
                new CatPriceType(){ ShortName="Aktion", Description="Aktionspreis", ValidFrom=new DateTime(2021, 01, 01) },
                new CatPriceType(){ ShortName="Normal", Description="Normalpreis", ValidFrom=new DateTime(2021, 01, 01) },
            };
            CatPriceTypes.AddRange(catPriceTypes);
            SaveChanges();


            List<Customer> customers = new Faker<Customer>("de").CustomInstantiator(f =>
                new Customer(
                    f.Random.Enum<Genders>(),
                    f.Random.Long(111111, 999999),
                    f.Name.FirstName(Bogus.DataSets.Name.Gender.Female),
                    f.Name.LastName(),
                    f.Internet.Email(),
                    f.Date.Between(DateTime.Now.AddYears(-60), DateTime.Now.AddYears(-16)),
                    f.Date.Between(DateTime.Now.AddYears(-10), DateTime.Now.AddDays(-2))
                ))
                .Rules((f, c) =>
                {
                    if (c.Gender == Genders.Male)
                    {
                        c.FirstName = f.Name.FirstName(Bogus.DataSets.Name.Gender.Male);
                    }
                    c.Address = new Address(
                        f.Address.StreetName(),
                        f.Address.BuildingNumber(),
                        f.Address.ZipCode(),
                        f.Address.City());
                    c.PhoneNumber = f.Phone.PhoneNumber();
                    c.LastChangeDate = f.Date.Between(new DateTime(2020, 01, 01), DateTime.Now).Date.OrNull(f, 0.3f);
                })
                .Generate(30)
                .ToList();

            Customers.AddRange(customers);
            SaveChanges();


            List<Shop> shops = new Faker<Shop>("de").CustomInstantiator(f =>
                new Shop(
                    f.Company.CompanySuffix(),
                    f.Company.CompanyName(),
                    f.Address.StreetAddress(),
                    f.Company.CatchPhrase(),
                    f.Company.Bs(),
                    new Address(f.Address.StreetName(), f.Address.BuildingNumber(), f.Address.ZipCode(), f.Address.City()),
                    f.Random.Guid()
            ))
            .Rules((f, s) =>
            {
                DateTime? lastChangeDate = f.Date.Between(new DateTime(2020, 01, 01), DateTime.Now).Date.OrNull(f, 0.3f);
                s.LastChangeDate = lastChangeDate;
            })
            .Generate(5)
            .ToList();
            Shops.AddRange(shops);
            SaveChanges();


            List<Category> categories = new Faker<Category>("de").CustomInstantiator(f =>
                new Category("", f.Random.ListItem(shops)) //f.Random.ListItem(f.Commerce.Categories)
            )
            .Rules((f, c) =>
            {
                c.Name = f.Random.ArrayElement(f.Commerce.Categories(10));
                c.LastChangeDate = f.Date.Between(new DateTime(2020, 01, 01), DateTime.Now).Date.OrNull(f, 0.3f);
            })
            .Generate(30)
            .ToList();
            Categories.AddRange(categories);
            SaveChanges();


            List<Product> products = new Faker<Product>("de").CustomInstantiator(f =>
                new Product(
                    f.Commerce.ProductName(),
                    20,
                    f.Commerce.Ean13(),
                    f.Commerce.ProductMaterial(),
                    f.Date.Between(new DateTime(2022, 01, 01), new DateTime(2024, 12, 31)),
                    f.Random.ListItem(categories))
            )
            .Rules((f, p) =>
            {
                DateTime? deliverydate = p.ExpiryDate?.Date.OrNull(f, 0.5f);
                DateTime? lastChangeDate = f.Date.Between(new DateTime(2020, 01, 01), DateTime.Now).Date.OrNull(f, 0.3f);
            })
            .Generate(500)
            .GroupBy(t => t.Name)
            .Select(g => g.First())
            .ToList();
            Products.AddRange(products);
            SaveChanges();


            List<Price> prices = new Faker<Price>().CustomInstantiator(f =>
                new Price(
                    f.Random.Decimal(10, 500),
                    20,
                    f.Random.ListItem(products),
                    catPriceTypes[0]))
            .Rules((f, pr) =>
            {
                pr.LastChangeDate = f.Date.Between(new DateTime(2020, 01, 01), DateTime.Now).Date.OrNull(f, 0.3f);
            })
            .Generate(500)
            .ToList();
            Prices.AddRange(prices);
            SaveChanges();


            List<Price> prices2 = new Faker<Price>().CustomInstantiator(f =>
                new Price(
                    f.Random.Decimal(10, 500),
                    20,
                    f.Random.ListItem(products),
                    catPriceTypes[1]))
            .Rules((f, pr) =>
            {
                pr.LastChangeDate = f.Date.Between(new DateTime(2020, 01, 01), DateTime.Now).Date.OrNull(f, 0.3f);
            })
            .Generate(10)
            .ToList();
            Prices.AddRange(prices2);
            SaveChanges();


            List<ShoppingCart> shoppingCarts = new Faker<ShoppingCart>("de").CustomInstantiator(f =>
                new ShoppingCart(
                    f.Commerce.ProductName(),
                    ShoppingCartStates.Sent,
                    f.Date.Between(DateTime.Now.AddYears(-10), DateTime.Now.AddDays(-10)),
                    f.Random.ListItem(customers),
                    f.Random.Guid())
            )
            .Rules((f, s) =>
            {
               s.LastChangeDate = f.Date.Between(new DateTime(2020, 01, 01), DateTime.Now).Date.OrNull(f, 0.3f);
            })
            .Generate(200)
            .GroupBy(t => t.Guid)
            .Select(g => g.First())
            .ToList();
            ShoppingCarts.AddRange(shoppingCarts);
            SaveChanges();


            //for (int i = 0; i <= 20; i++)
            //{
            //    Product product = new Faker().Random.ListItem(Products.ToList());
            //    ShoppingCart shoppingCart = new Faker().Random.ListItem(ShoppingCarts.ToList());
            //    ShoppingCartItem shoppingCartItem = new ShoppingCartItem(name: product.Name, tax: product.Prices[0].Tax, nett: product.Prices[0].Nett, shoppingCart: shoppingCart, product: product);
            //    shoppingCart.AddItem(shoppingCartItem);
            //    SaveChanges();
            //}
        }
    }
}
