using Moq;
using Spg.SpengerShop.Application.Products;
using Spg.SpengerShop.Application.Test.Helpers;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spg.SpengerShop.ServicesExtensions;
using Spg.SpengerShop.Domain.Dtos;

namespace Spg.SpengerShop.Application.Test.Mock
{
    public class ProductServiceLoadMock
    {
        private readonly Mock<IDateTimeService> _dateTimeService = new Mock<IDateTimeService>();
        private readonly Mock<IReadOnlyRepositoryBase<Product>> _readOnlyProductRepository = new Mock<IReadOnlyRepositoryBase<Product>>();
        private readonly Mock<IReadOnlyRepositoryBase<Category>> _readOnlyCategoryRepository = new Mock<IReadOnlyRepositoryBase<Category>>();
        private readonly Mock<IProductRepository> _productRepository = new Mock<IProductRepository>();
        private readonly IReadOnlyProductService _unitToTest;

        public ProductServiceLoadMock()
        {
            _unitToTest = new ProductService(
                _productRepository.Object,
                _readOnlyProductRepository.Object,
                _readOnlyCategoryRepository.Object,
                _dateTimeService.Object);
        }

        [Fact]
        public void Create_Product_Success_Test()
        {
            // Arrange
            _dateTimeService.Setup(d => d.Now).Returns(new DateTime(2023, 02, 28));
            _readOnlyProductRepository
                .Setup(r => r.GetAll(null, "", null, null))
                .Returns(MockUtilities.GetSeedingProducts(MockUtilities.GetSeedingCategory(MockUtilities.GetSeedingShop())).AsQueryable());
            string filter = "t";
            _readOnlyProductRepository
                .Setup(r => r.Get(p => p.Name.ToLower().Contains(filter.ToLower()), null, "", null, null))
                .Returns(MockUtilities.GetSeedingProducts(MockUtilities.GetSeedingCategory(MockUtilities.GetSeedingShop())).AsQueryable());

            // Act
            List<ProductDto> actual = _unitToTest
                .Load()
                .UseFilterContainsName("t")
                .UseFilterByExpiryDate(DateTime.Now, DateTime.Now)
                .UseSorting("asd")
                .GetData()
                .ToList();

            // Assert
            Assert.Equal(5, actual.Count());
        }
    }
}
