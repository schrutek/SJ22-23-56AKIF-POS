using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Spg.SpengerShop.Application.Helpers;
using Spg.SpengerShop.Application.Products;
using Spg.SpengerShop.Application.Test.Helpers;
using Spg.SpengerShop.Domain.Dtos;
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
                new RepositoryBase<Product>(db),
                new RepositoryBase<Category>(db),
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

                NewProductDto entity = new NewProductDto() 
                { 
                    Name = "Test Product 1", 
                    Tax = 10, Ean = "1234567891234", 
                    Material = "MyProduct Material 1",
                    ExpiryDate = new DateTime(2023, 03, 17),
                    CategoryId = new Guid("d2616f6e-7424-4b9f-bf81-6aad88183f41")
                };

                // Act
                unitToTest.Create(entity);

                // Assert
                Assert.Equal(2, db.Products.ToList().Count());
                Assert.Equal("Test Product 1", db.Products.ToList().ElementAt(1).Name);
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

                NewProductDto entity = new NewProductDto()
                {
                    Name = "Test Product 99",
                    Tax = 10,
                    Ean = "1234567891234",
                    Material = "MyProduct Material 1",
                    ExpiryDate = new DateTime(2023, 03, 17),
                    CategoryId = new Guid("d2616f6e-7424-4b9f-bf81-6aad88183f41")
                };

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

                NewProductDto entity = new NewProductDto()
                {
                    Name = "Test Product 10",
                    Tax = 10,
                    Ean = "1234567891234",
                    Material = "MyProduct Material 1",
                    ExpiryDate = new DateTime(2023, 03, 12),
                    CategoryId = new Guid("d2616f6e-7424-4b9f-bf81-6aad88183f41")
                };

                // Act + Assert
                Assert.Throws<CreateProductServiceException>(() => unitToTest.Create(entity));
            }
        }
    }
}