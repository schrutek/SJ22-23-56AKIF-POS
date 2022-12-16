using Bogus;
using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Infrastructure
{
    // 1. Diese Klasse muss von DBContext ableiten
    public class SpengerShopContext : DbContext
    {
        // 2. Die Tabellen der DB als Properties auflisten
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Product> Products => Set<Product>();
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
        }

        public void Seed()
        {
            Randomizer.Seed = new Random(144412);

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
                    c.Address = new Address()
                    {
                        City = f.Address.City(),
                        Number = f.Address.BuildingNumber(),
                        Street = f.Address.StreetName(),
                        Zip = f.Address.ZipCode()
                    };
                    c.PhoneNumber= f.Phone.PhoneNumber();
                })
                .Generate(30)
                .ToList();

            Customers.AddRange(customers);
            SaveChanges();

            List<ShoppingCart> shoppingCarts = new Faker<ShoppingCart>().CustomInstantiator(f => 
                new ShoppingCart(
                    f.Commerce.ProductName(),
                    ShoppingCartStates.Sent,
                    f.Date.Between(DateTime.Now.AddYears(-10), DateTime.Now.AddDays(-10))
                )
            ).Rules((f, s) => 
            {
                s.CustomerNavigation = f.Random.ListItem(customers);
            })
            .Generate(200)
            .ToList();

            ShoppingCarts.AddRange(shoppingCarts);
            SaveChanges();
        }
    }
}
