using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using Spg.SpengerShop.Application.Helpers;
using Spg.SpengerShop.Application.Products;
using Spg.SpengerShop.Application.Test.Helpers;
using Spg.SpengerShop.Domain.Dtos;
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
        private readonly Mock<IReadOnlyRepositoryBase<Product>> _readOnlyProductRepository = new Mock<IReadOnlyRepositoryBase<Product>>();
        private readonly Mock<IReadOnlyRepositoryBase<Category>> _readOnlyCategoryRepository = new Mock<IReadOnlyRepositoryBase<Category>>();
        private readonly Mock<IProductRepository> _productRepository = new Mock<IProductRepository>();
        private readonly IAddableProductService _unitToTest;

        public ProducktServiceMockTest()
        {
            _unitToTest = new ProductService(
                _productRepository.Object, 
                _readOnlyProductRepository.Object,
                _readOnlyCategoryRepository.Object,
                _dateTimeService.Object);
        }

        [Fact]
        public void CreateProduct_Success_Test()
        {
            // Arrange
            NewProductDto dto = new NewProductDto()
            {
                Name = "Test Product 1",
                Tax = 10,
                Ean = "1234567891234",
                Material = "MyProduct Material 1",
                ExpiryDate = new DateTime(2023, 03, 17),
                CategoryId = new Guid("d2616f6e-7424-4b9f-bf81-6aad88183f41")
            };
            Product entity = new Product(
                "Test Product 1", 
                10, 
                "1234567891234", 
                "MyProduct Material 1",
                new DateTime(2023, 03, 17), null!
            );

            _dateTimeService.Setup(d => d.Now).Returns(new DateTime(2023, 02, 28));
            _readOnlyProductRepository
                .Setup(r => r.GetByPK("Test Product 99"))
                .Returns(MockUtilities.GetSeedingProduct(MockUtilities.GetSeedingCategory(MockUtilities.GetSeedingShop())));
            _productRepository.Setup(r => r.Create(entity));

            // Act
            _unitToTest.Create(dto);

            // Assert
            _productRepository.Verify(r => r.Create(It.IsAny<Product>()), Times.Once);
        }
    }
}
