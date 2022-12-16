using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Test
{
    public class ProductTest
    {
        private SpengerShopContext GenerateDb()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlite("Data Source=SpengerShop_Test.db");

            SpengerShopContext db = new SpengerShopContext(optionsBuilder.Options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }

        [Fact]
        public void SeedDb()
        {
            SpengerShopContext db = GenerateDb();
            db.Seed();
            Assert.True(true);
        }

        [Fact]
        public void Product_Add_OneEntity_SuccessTest()
        {
            // AAA
            // 1. Arrange
            SpengerShopContext db = GenerateDb();
            Product newProduct = new Product("Testprodukt", 12.50M, 20, "1324567890123", "Material", DateTime.UtcNow.AddDays(14));

            // 2. Act
            db.Products.Add(newProduct);
            db.SaveChanges();
            
            // 3. Assert
            Assert.Equal(1, db.Products.Count());
        }
    }
}
