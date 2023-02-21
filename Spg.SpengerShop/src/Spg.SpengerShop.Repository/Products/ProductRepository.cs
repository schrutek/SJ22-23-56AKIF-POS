using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Domain.Exceptions;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Repository.Products
{
    public class ProductRepository : IProductRepository, IReadOnlyProductRepository
    {
        private readonly SpengerShopContext _db;

        public ProductRepository(SpengerShopContext db)
        {
            _db = db;
        }

        public void Create(Product newProduct)
        {
            try
            {
                _db.Products.Add(newProduct);
                _db.SaveChanges(); // => Insert
            }
            catch (NullReferenceException ex)
            {
                throw new ProductRepositoryCreateException("Create nicht möglich!", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new ProductRepositoryCreateException("Create nicht möglich!", ex);
            }
        }

        public IQueryable<Product> GetAll()
        {
            return _db.Products;
        }
    }
}
