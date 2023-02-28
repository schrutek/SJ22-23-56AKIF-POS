using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Domain.Exceptions;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using Spg.SpengerShop.Repository;
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
                new RepositoryBase<Product>(db).Create(entity);

                // Assert
                Assert.Single(db.Products.ToList());
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
                Assert.Throws<ProductRepositoryCreateException>(() => new RepositoryBase<Product>(db).Create(entity));
            }
        }
    }
}