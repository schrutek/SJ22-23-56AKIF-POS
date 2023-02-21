using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Domain.Exceptions;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using Spg.SpengerShop.Repository.Products;

namespace Spg.SpengerShop.RepositoryTest
{
    public class ProductRepositoryTest
    {
        private SpengerShopContext GenerateDb()
        {
            DbContextOptionsBuilder options = new DbContextOptionsBuilder();
            options.UseSqlite("Data Source=./SpengerShop_Test.db");

            SpengerShopContext db = new SpengerShopContext(options.Options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }

        [Fact]
        public void Create_Success_Test()
        {
            // Arrange (Enty, DB)
            SpengerShopContext db = GenerateDb();

            Shop shop = new Shop("GMBH", "Test Shop", "Test Location", "IDontKnow", "Bs", new Address("Spengergasse", "20", "1050", "Wien"), new Guid("0c03ceb5-e2a2-4faf-b273-63839505f573"));
            Category category = new Category("Test Category", shop);
            Product entity = new Product("Test Product", 10, "1234567891234", "MyProduct Material", new DateTime(2023, 03, 17), category);

            // Act
            new ProductRepository(db).Create(entity);

            // Assert
            Assert.Single(db.Products.ToList());
        }

        [Fact]
        public void Create_ProductRepositoryCreateException_Test()
        {
            // Arrange (Enty, DB)
            SpengerShopContext db = GenerateDb();

            Shop shop = new Shop("GMBH", "Test Shop", "Test Location", "IDontKnow", "Bs", new Address("Spengergasse", "20", "1050", "Wien"), new Guid("0c03ceb5-e2a2-4faf-b273-63839505f573"));
            Category category = new Category("Test Category", shop);
            Product entity = new Product("Test Product", 10, "1234567891234", "MyProduct Material", new DateTime(2023, 03, 17), category);

            // Assert
            Assert.Throws<ProductRepositoryCreateException>(() => new ProductRepository(db).Create(null));
        }
    }
}