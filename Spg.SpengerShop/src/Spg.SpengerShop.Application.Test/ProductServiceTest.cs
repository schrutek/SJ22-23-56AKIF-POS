using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Spg.SpengerShop.Application.Helpers;
using Spg.SpengerShop.Application.Products;
using Spg.SpengerShop.Application.Test.Helpers;
using Spg.SpengerShop.Domain.Exceptions;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using Spg.SpengerShop.Repository;
using Spg.SpengerShop.Repository.Products;

namespace Spg.SpengerShop.Application.Test
{
    public class ProductServiceTest
    {
        private ProductService InitUnitToTest(SpengerShopContext db)
        {
            return new ProductService(
                new ProductRepository(db), 
                new ProductRepository(db),
                new DateTimeServiceMock());
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
                Assert.Equal(2, db.Products.ToList().Count());
                //Assert.Equal("Test Product 1", db.Products.OrderBy(p => p.Name).Last().Name);
            }
        }

        [Fact]
        public void CreateProduct_NameNotUnique_CreateProductServiceException_Test()
        {
            // Arrange
            using (SpengerShopContext db = new SpengerShopContext(GenerateDbOptions()))
            {
                ProductService unitToTest = InitUnitToTest(db);

                DatabaseUtilities.InitializeDatabase(db);

                Product entity = new Product("Test Product 99", 10, "1234567891234", "MyProduct Material 99",
                    new DateTime(2023, 03, 17), db.Categories.Single(s => s.Id == 1));

                // Act + Assert
                Assert.Throws<CreateProductServiceException>(() => unitToTest.Create(entity));
            }
        }

        [Fact]
        public void CreateProduct_ExpiaryDate_NotInFuture_CreateProductServiceException_Test()
        {
            // Arrange
            using (SpengerShopContext db = new SpengerShopContext(GenerateDbOptions()))
            {
                ProductService unitToTest = InitUnitToTest(db);

                DatabaseUtilities.InitializeDatabase(db);

                Product entity = new Product("Test Product 100", 10, "1234567891234", "MyProduct Material 99",
                    new DateTime(2023, 03, 12), db.Categories.Single(s => s.Id == 1));

                // Act + Assert
                Assert.Throws<CreateProductServiceException>(() => unitToTest.Create(entity));
            }
        }
    }
}