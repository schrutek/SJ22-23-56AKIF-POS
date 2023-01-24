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
    public class TestProductRepository : IProductRepository
    {
        private readonly SpengerShopContext _db;

        public TestProductRepository(SpengerShopContext db)
        {
            _db = db;
        }

        public IEnumerable<Product> GetAll()
        {
            return _db.Products;
        }
    }
}
