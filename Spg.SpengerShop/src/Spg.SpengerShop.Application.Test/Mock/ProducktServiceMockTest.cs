using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using Spg.SpengerShop.Application.Helpers;
using Spg.SpengerShop.Application.Products;
using Spg.SpengerShop.Application.Test.Helpers;
using Spg.SpengerShop.Domain.Exceptions;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using Spg.SpengerShop.Repository.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Application.Test.Mock
{
    public class ProducktServiceMockTest
    {
        private readonly Mock<IDateTimeService> _dateTimeService = new Mock<IDateTimeService>();

        private readonly IAddableProductService _unitToTest = null;

        public ProducktServiceMockTest()
        {
            _unitToTest = new ProductService(null, null, _dateTimeService.Object);
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
                _dateTimeService.Setup(d => d.Now).Returns(new DateTime(2023, 02, 28));

                DatabaseUtilities.InitializeDatabase(db);

                Product entity = new Product("Test Product 1", 10, "1234567891234", "MyProduct Material 1",
                    new DateTime(2023, 03, 17), db.Categories.Single(s => s.Id == 1));

                // Act
                _unitToTest.Create(entity);

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
                _dateTimeService.Setup(d => d.Now).Returns(new DateTime(2023, 02, 28));

                DatabaseUtilities.InitializeDatabase(db);

                Product entity = new Product("Test Product 99", 10, "1234567891234", "MyProduct Material 99",
                    new DateTime(2023, 03, 17), db.Categories.Single(s => s.Id == 1));

                // Act + Assert
                Assert.Throws<CreateProductServiceException>(() => _unitToTest.Create(entity));
            }
        }

        [Fact]
        public void CreateProduct_ExpiaryDate_NotInFuture_CreateProductServiceException_Test()
        {
            // Arrange
            using (SpengerShopContext db = new SpengerShopContext(GenerateDbOptions()))
            {
                _dateTimeService.Setup(d => d.Now).Returns(new DateTime(2023, 02, 28));

                DatabaseUtilities.InitializeDatabase(db);

                Product entity = new Product("Test Product 100", 10, "1234567891234", "MyProduct Material 99",
                    new DateTime(2023, 03, 12), db.Categories.Single(s => s.Id == 1));

                // Act + Assert
                Assert.Throws<CreateProductServiceException>(() => _unitToTest.Create(entity));
            }
        }
    }
}
