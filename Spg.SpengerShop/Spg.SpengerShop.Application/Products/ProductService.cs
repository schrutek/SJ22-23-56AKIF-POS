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


        public IEnumerable<Product> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
