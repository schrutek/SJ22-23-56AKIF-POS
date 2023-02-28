using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Repository.Products
{
    public class TestProductRepository : IRepositoryBase<Product>
    {
        private readonly SpengerShopContext _db;

        public TestProductRepository(SpengerShopContext db)
        {
            _db = db;
        }

        public void Create(Product newProduct)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Product> GetAll()
        {
            return _db.Products;
        }
    }
}
