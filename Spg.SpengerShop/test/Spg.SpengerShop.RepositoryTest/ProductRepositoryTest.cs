using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using Spg.SpengerShop.Domain.Exceptions;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using Spg.SpengerShop.Repository;
using Spg.SpengerShop.Repository.Products;
using Spg.SpengerShop.RepositoryTest.Helpers;

namespace Spg.SpengerShop.RepositoryTest
{
    public class ProductRepositoryTest
    {
        private readonly Mock<SpengerShopContext> _db = new Mock<SpengerShopContext>();

        public ProductRepositoryTest()
        {
            IRepositoryBase<Product> _product = new RepositoryBase<Product>(_db.Object);
        }

        [Fact]
        public void Create_Success_Test()
        {
            // Arrange (Enty, DB)
            using (SpengerShopContext db = new SpengerShopContext(DatabaseUtilities.GenerateDbOptions()))
            {
                DatabaseUtilities.InitializeDatabase(db);

                Product entity = new Product("Test Product 1", 10, "1234567891234", "MyProduct Material", new DateTime(2023, 03, 17), db.Categories.Single(c => c.Id == 1));

                // Act
                new ProductRepository(db).Create(entity);

                // Assert
                Assert.Equal(2, db.Products.Count());
            }
        }

        [Fact]
        public void Create_ProductRepositoryCreateException_Test()
        {
            // Arrange (Enty, DB)
            using (SpengerShopContext db = new SpengerShopContext(DatabaseUtilities.GenerateDbOptions()))
            {

                DatabaseUtilities.InitializeDatabase(db);

                Category newCategory = new Category("", new Guid("d2616f6e-7424-4b9f-bf81-6aad88183f41"), null);
                Product entity = new Product("Test Product", 10, "1234567891234", "MyProduct Material", new DateTime(2023, 03, 17), newCategory);

                // Assert
                Assert.Throws<ProductRepositoryCreateException>(() => new ProductRepository(db).Create(entity));
            }
        }

        [Fact]
        public void Product_GetByPK_Success_Test()
        {
            // Arrange (Enty, DB)
            using (SpengerShopContext db = new SpengerShopContext(DatabaseUtilities.GenerateDbOptions()))
            {
                DatabaseUtilities.InitializeDatabase(db);

                Product expected = new Product("Test Product 99", 20, "1234567890123", "Testmaterial", new DateTime(2023, 03, 17), db.Categories.Single(c => c.Id == 1));

                // Act
                Product actual = new RepositoryBase<Product>(db).GetByPK("Test Product 99")!;

                // Assert
                Assert.Equal(expected.Name, actual.Name);
                Assert.Equal(expected.Ean, actual.Ean);
            }
        }

        [Fact]
        public void Category_GetByPK_Success_Test()
        {
            // Arrange (Enty, DB)
            using (SpengerShopContext db = new SpengerShopContext(DatabaseUtilities.GenerateDbOptions()))
            {
                DatabaseUtilities.InitializeDatabase(db);

                Category expected = new Category("DVD", new Guid("d2616f6e-7424-4b9f-bf81-6aad88183f41"), null!);

                // Act
                Category actual = new RepositoryBase<Category>(db).GetByPK(1)!;

                // Assert
                Assert.Equal(expected.Name, actual.Name);
            }
        }

        [Fact]
        public void Customer_GetByGuid_Success_Test()
        {
            // Arrange (Enty, DB)
            using (SpengerShopContext db = new SpengerShopContext(DatabaseUtilities.GenerateDbOptions()))
            {
                DatabaseUtilities.InitializeDatabase(db);

                Customer expected = new Customer(
                    new Guid("6ecfca13-f862-4c74-ac0e-30a2a62dd128"), 
                    Genders.Male, 123, "FirstName", "LastName", "test@test.at", 
                    new DateTime(1977, 05, 13), new DateTime(2023, 02, 01), 
                    new Address("", "", "", ""));

                // Act
                Customer actual = new RepositoryBase<Customer>(db).GetByGuid<Customer>(new Guid("6ecfca13-f862-4c74-ac0e-30a2a62dd128"))!;

                // Assert
                Assert.Equal(new Guid("6ecfca13-f862-4c74-ac0e-30a2a62dd128"), actual.Guid);
            }
        }

        //[Fact]
        //public void Create_Success_TestMock()
        //{
        //    _db.Setup(d=>d.Products.SingleOrDefault(p=>p.Name == "")).Returns("Test Product 1", 10, "1234567891234", "MyProduct Material", new DateTime(2023, 03, 17), null!);

        //    Product entity = new Product("Test Product 1", 10, "1234567891234", "MyProduct Material", new DateTime(2023, 03, 17), db.Categories.Single(c => c.Id == 1));

        //    // Act
        //    new ProductRepository(db).Create(entity);

        //    // Assert
        //    Assert.Equal(2, db.Products.Count());
        //}

    }
}