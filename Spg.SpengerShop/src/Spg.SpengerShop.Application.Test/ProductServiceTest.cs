using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Spg.SpengerShop.Application.Products;
using Spg.SpengerShop.Application.Test.Helpers;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using Spg.SpengerShop.Repository;

namespace Spg.SpengerShop.Application.Test
{
    public class ProductServiceTest
    {
        private ProductService InitUnitToTest(SpengerShopContext db)
        {
            return new ProductService(
                new RepositoryBase<Product>(db), 
                new RepositoryBase<Product>(db));
        }

        private DbContextOptions GenerateDbOptions()
        {
            SqliteConnection connection = new SqliteConnection("Data Source = :memory:");
            connection.Open();

            DbContextOptionsBuilder options = new DbContextOptionsBuilder();
            options.UseSqlite(connection);
            return options.Options;
        }

        [Fact]
        public void CreateProduct_Success_Test()
        {
            // Arrange
            using (SpengerShopContext db = new SpengerShopContext(GenerateDbOptions()))
            {
                ProductService unitToTest = InitUnitToTest(db);

                DatabaseUtilities.InitializeDatabase(db);

                Product entity = new Product("Test Product 1", 10, "1234567891234", "MyProduct Material 1",
                    new DateTime(2023, 03, 17), db.Categories.Single(s => s.Id == 1));

                // Act
                unitToTest.Create(entity);

                // Assert
                Assert.Equal(1, db.Products.ToList().Count());
                Assert.Equal("Test Product 1", db.Products.First().Name);
            }
        }
    }
}