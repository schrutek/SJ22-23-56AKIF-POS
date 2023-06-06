using Bogus.DataSets;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Application.CQRS.Products.FilterByExpiryDate.Queries;
using Spg.SpengerShop.Application.CQRS.Products.GetByName.Queries;
using Spg.SpengerShop.Domain.Dtos;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using Spg.SpengerShop.ServicesExtensions;

namespace Spg.SpengerShop.MvcFrontEnd.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IReadOnlyProductService _readOnlyProductService;
        private readonly IAddableProductService _addableProductService;
        private readonly IMediator _mediator;

        private readonly SpengerShopContext _db;

        public ProductsController(
            IReadOnlyProductService readOnlyProductService, 
            IAddableProductService addableProductService,
            IMediator mediator,
            SpengerShopContext db)
        {
            _readOnlyProductService = readOnlyProductService;
            _addableProductService = addableProductService;
            _mediator = mediator;
            _db = db;
        }

        // /Product/Index
        // Mittels CRUD
        [HttpGet()]
        public IActionResult Index()
        {
            IEnumerable<ProductDto> model = _readOnlyProductService
                .Load()
                .UseFilterContainsName("rub")
                .GetData();

            return View(model);
        }
        
        // /Product/Details/Ergonomic%20Rubber%20Car
        // Mittels Mediator & CQRS
        [HttpGet()]
        public IActionResult Details(string id, string? state)
        {
            // Würde für FT ausreichen
            //Product product = _db
            //    .Products
            //    .Include(p => p.ShoppingCartItems)
            //    .SingleOrDefault(p => p.Name == id);

            //ProductDto model = new ProductDto(
            //    product.Name, 
            //    product.Tax, 
            //    product.Ean, 
            //    product.Material, 
            //    product.ExpiryDate, 
            //    product.ShoppingCartItems.Select(i => new ShoppingCartItemDto(i.Id, null!,
            //        new ProductDto(
            //            i.ProductNavigation.Name,
            //            i.ProductNavigation.Tax,
            //            i.ProductNavigation.Ean,
            //            i.ProductNavigation.Material,
            //            i.ProductNavigation.ExpiryDate,
            //            null!)!))
            //    .ToList());

            // Etwas saubere Variante mit Service
            ProductDto model = _mediator
                .Send(new GetProductByNameRequest(id))
                .Result;
            return View(model);
        }

        // /Products/Expires
        [HttpGet()]
        public IActionResult Expires()
        {
            IQueryable<Product> model = _mediator
                .Send(new GetByExpiryDateRequest(DateTime.Now.AddDays(14)))
                .Result;

            // Bad Coding, bitte besser machen
            return View("Expires", model.Select(p => new ProductDto(p.Name, p.CategoryId, p.Ean, p.Material, p.ExpiryDate)));
        }

        [HttpPost()]
        public IActionResult Create(NewProductDto newProduct)
        {
            if (ModelState.IsValid)
            {

            }
            _addableProductService.Create(newProduct);
            return View();
        }

        [HttpGet()]
        public IActionResult ConfirmDelete(string name)
        {
            // TODO: Seite mit Info "wirklich löschen" anzeigen
            return View();
        }

        [HttpPost()]
        public IActionResult Delete(string name)
        {
            // TODO: Logik zum löschen in der DB
            return View("DeleteDone");
        }
    }
}
