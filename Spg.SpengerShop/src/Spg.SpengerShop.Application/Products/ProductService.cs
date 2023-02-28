using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Application.Products
{
    public class ProductService : IReadOnlyProductService, IAddableProductService, IUpdateableProductService
    {
        private readonly IRepositoryBase<Product> _repository;
        private readonly IReadOnlyRepositoryBase<Product> _readOnlyProductRepository;

        public ProductService(IRepositoryBase<Product> repository, IReadOnlyRepositoryBase<Product> readOnlyProductRepository)
        {
            _repository = repository;
            _readOnlyProductRepository = readOnlyProductRepository;
        }

        public IQueryable<Product> GetAll()
        {
            IQueryable<Product> products = _readOnlyProductRepository.GetAll();
            return products;
        }

        // Kommt später dran
        public void Create(Product newProduct)
        {
            // * Es muss unique sein
            // * ExpiaryDate muss in der Zukunft liegen

            // * Es muss eine gültige Kategorie haben
            // * Das Auslieferungsdatum darf nicht an einem Samstag/Sonntag sein
            // * Stock darf nicht kleiner 1 sein.
            // * Das Ablaufdatum darf nicht in den nächsten 2 Wochen sein
            // * Das Ablaufdatum darf nicht in der Vergangenheit sein
            // * Der Preis muss > 1 sein
            // * Produkt darf nur in einer Kategorie vorkommen



            _repository.Create(newProduct);
        }

        public void Update(int id, Product product)
        {
            throw new NotImplementedException();
        }
    }
}
