using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Domain.Exceptions;
using Spg.SpengerShop.Domain.Interfaces;
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

        public void Create(Product newEntity)
        {
            try
            {
                DbSet<Product> dbSet = _db.Set<Product>();
                dbSet.Add(newEntity);
                _db.SaveChanges(); // => Insert
            }
            catch (DbUpdateException ex)
            {
                throw new ProductRepositoryCreateException("Create nicht möglich!", ex);
            }
        }

        public Product? GetByName(string name)
        {
            return _db.Products.SingleOrDefault(e => e.Name == name);
        }

        public IQueryable<Product> GetAll()
        {
            return _db.Set<Product>();
        }
    }
}
