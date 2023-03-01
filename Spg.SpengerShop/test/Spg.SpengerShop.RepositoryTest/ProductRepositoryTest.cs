using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Domain.Exceptions;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using Spg.SpengerShop.Repository;
using Spg.SpengerShop.Repository.Products;
using Spg.SpengerShop.RepositoryTest.Helpers;

namespace Spg.SpengerShop.RepositoryTest
{
    public class ProductRepositoryTest
    {
        private DbContextOptions GenerateDbOptions()
        {
            SqliteConnection connection = new SqliteConnection("Data Source = :memory:");
            connection.Open();

            DbContextOptionsBuilder options = new DbContextOptionsBuilder();
            options.UseSqlite(connection);
            return options.Options;
        }

        [Fact]
        public void Create_Success_Test()
        {
            // Arrange (Enty, DB)
            using (SpengerShopContext db = new SpengerShopContext(GenerateDbOptions()))
            {
                DatabaseUtilities.InitializeDatabase(db);

                Product entity = new Product("Test Product", 10, "1234567891234", "MyProduct Material", new DateTime(2023, 03, 17), db.Categories.Single(c => c.Id == 1));

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
            using (SpengerShopContext db = new SpengerShopContext(GenerateDbOptions()))
            {

                DatabaseUtilities.InitializeDatabase(db);

                Category newCategory = new Category("", null);
                Product entity = new Product("Test Product", 10, "1234567891234", "MyProduct Material", new DateTime(2023, 03, 17), newCategory);

                // Assert
                Assert.Throws<ProductRepositoryCreateException>(() => new ProductRepository(db).Create(entity));
            }
        }

        [Fact]
        public void GetByName_Success_Test()
        {
            // Arrange (Enty, DB)
            using (SpengerShopContext db = new SpengerShopContext(GenerateDbOptions()))
            {
                DatabaseUtilities.InitializeDatabase(db);

                Product expected = new Product("Test Product 99", 10, "1234567891234", "MyProduct Material", new DateTime(2023, 03, 17), db.Categories.Single(c => c.Id == 1));

                // Act
                Product actual = new ProductRepository(db).GetByName("Test Product 99");

                // Assert
                Assert.Equal(expected.Name, actual.Name);
                Assert.Equal(expected.Ean, actual.Ean);
            }
        }

    }
}