using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Repository.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Application.Products
{
    public class ProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        // Method Injection
        //public void SetProductRepository(IProductRepository repository)
        //{
        //    _repository = repository;
        //}

        //// Property Injection
        //public IProductRepository Repository { get { return _repository; } set { _repository = value; } }


        public IQueryable<Product> GetAll(string filter)
        {
            IQueryable<Product> products = _repository.GetAll();
            products = products.Where(p => p.Name.Contains(filter));
            return products;
        }
    }
}
