using Spg.SpengerShop.Domain.Dtos;
using Spg.SpengerShop.Domain.Exceptions;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Repository;
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
        private readonly IReadOnlyRepositoryBase<Product> _readOnlyProductRepository;
        private readonly IReadOnlyRepositoryBase<Category> _readOnlyCategoryRepository;
        private readonly IDateTimeService _dateTimeService;

        public ProductService(IProductRepository repository,
            IReadOnlyRepositoryBase<Product> readOnlyProductRepository,
            IReadOnlyRepositoryBase<Category> readOnlyCategoryRepository,
            IDateTimeService dateTimeService)
        {
            _repository = repository;
            _readOnlyProductRepository = readOnlyProductRepository;
            _readOnlyCategoryRepository = readOnlyCategoryRepository;
            _dateTimeService = dateTimeService;
        }

        public IQueryable<Product> GetAll()
        {
            IQueryable<Product> products = _readOnlyProductRepository.GetAll();
            return products;
        }

        // Kommt später dran
        /// <summary>
        // * Es muss eine gültige Kategorie haben
        // * Das Auslieferungsdatum darf nicht an einem Samstag/Sonntag sein
        // * Stock darf nicht kleiner 1 sein.
        // * Das Ablaufdatum darf nicht in den nächsten 2 Wochen sein
        // * Das Ablaufdatum darf nicht in der Vergangenheit sein
        // * Der Preis muss > 1 sein
        // * Produkt darf nur in einer Kategorie vorkommen
        // * Nur der Admin darf ein Produkt anlegen
        /// </summary>
        /// <param name="newProduct"></param>
        /// <exception cref="CreateProductServiceException"></exception>
        public void Create(NewProductDto newProductDto)
        {
            // Init
            Category category;
            try
            {
                category = _readOnlyCategoryRepository.GetByGuid<Category>(newProductDto.CategoryId)
                    ?? throw new CreateProductServiceException("Kategorie wurde nicht gefunden!");
            }
            catch (InvalidOperationException ex)
            {
                // Logging
                throw new CreateProductServiceException("Kategorie ist mermals vorhanden!", ex);
            }

            // [ Mapping ]
            Product newProduct = new Product(
                newProductDto.Name, 
                newProductDto.Tax, 
                newProductDto.Ean, 
                newProductDto.Material, 
                newProductDto.ExpiryDate, 
                category);

            // Validation
            // * Es muss unique sein
            if (_readOnlyProductRepository.GetByPK(newProduct.Name) is not null)
            {
                throw new CreateProductServiceException("Produkt existiert bereits!");
            }
            // * ExpiaryDate muss in der Zukunft liegen
            if (newProduct.ExpiryDate.Value < _dateTimeService.Now.AddDays(14))
            {
                throw new CreateProductServiceException("Ablaufdatum muss 2 Wochen in der Zukuft liegen!");
            }

            // Act + Save
            _repository.Create(newProduct);

            // [Save] wird im Repository aufgerufen

            // [Mapping]
        }

        public void Update(int id, Product product)
        {
            throw new NotImplementedException();
        }
    }
}
