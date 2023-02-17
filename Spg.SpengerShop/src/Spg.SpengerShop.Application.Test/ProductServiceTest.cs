using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Application.Products;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using Spg.SpengerShop.Repository.Products;

namespace Spg.SpengerShop.Application.Test
{
    public class ProductServiceTest
    {
        private SpengerShopContext GenerateDb()
        {
            DbContextOptionsBuilder options = new DbContextOptionsBuilder();
            options.UseSqlite("Data Source=./SpengerShopt_Test.db");

            SpengerShopContext db = new SpengerShopContext(options.Options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }
        private ProductService InitUnitToTest(SpengerShopContext db)
        {
            return new ProductService(new ProductRepository(db));
        }

        [Fact]
        public void CreateProduct_Success_Test()
        {
            // Arrange
            SpengerShopContext db = GenerateDb();
            ProductService unitToTest = InitUnitToTest(db);

            Shop shop = new Shop("GMBH", "Test Shop", "Test Location", "IDontKnow", "Bs", new Address("Spengergasse", "20", "1050", "Wien"), new Guid("0c03ceb5-e2a2-4faf-b273-63839505f573"));
            Category category = new Category("Test Category", shop);
            Product entity = new Product("Test Product", 10, "1234567891234", "MyProduct Material", new DateTime(2023, 03, 17), category);

            // Act
            unitToTest.Create(entity);

            // Assert
            Assert.Equal(1, db.Products.Count());
            Assert.Equal("Test Product", db.Products.First().Name);
        }
    }
}