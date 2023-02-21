using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Repository.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Application.Products
{
    public class ProductService : IReadOnlyProductService, IAddableProductService, IUpdateableProductService
    {
        private readonly IProductRepository _repository;
        private readonly IReadOnlyProductRepository _readOnlyProductRepository;

        public ProductService(IProductRepository repository, IReadOnlyProductRepository readOnlyProductRepository)
        {
            _repository = repository;
            _readOnlyProductRepository = readOnlyProductRepository;
        }

        // Method Injection
        //public void SetProductRepository(IProductRepository repository)
        //{
        //    _repository = repository;
        //}

        //// Property Injection
        //public IProductRepository Repository { get { return _repository; } set { _repository = value; } }


        public IQueryable<Product> GetAll()
        {
            IQueryable<Product> products = _readOnlyProductRepository.GetAll();
            return products;
        }

        // Kommt später dran
        public void Create(Product newProduct)
        {
            _repository.Create(newProduct);
        }

        public void Update(int id, Product product)
        {
            throw new NotImplementedException();
        }
    }
}
