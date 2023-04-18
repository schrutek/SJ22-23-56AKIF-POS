using Bogus.DataSets;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Spg.SpengerShop.Application.CQRS.Products.FilterByExpiryDate.Queries;
using Spg.SpengerShop.Application.CQRS.Products.GetByName.Queries;
using Spg.SpengerShop.Domain.Dtos;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.ServicesExtensions;

namespace Spg.SpengerShop.MvcFrontEnd.Controllers
{
    public class ProductController : Controller
    {
        private readonly IReadOnlyProductService _readOnlyProductService;
        private readonly IAddableProductService _addableProductService;
        private readonly IMediator _mediator;

        public ProductController(
            IReadOnlyProductService readOnlyProductService, 
            IAddableProductService addableProductService,
            IMediator mediator)
        {
            _readOnlyProductService = readOnlyProductService;
            _addableProductService = addableProductService;
            _mediator = mediator;
        }

        // /Product/Index
        // Mittels CRUD
        [HttpGet()]
        public IActionResult Index()
        {
            IEnumerable<Product> model = _readOnlyProductService
                .Load()
                .UseFilterContainsName("rub")
                .GetData();

            return View(model);
        }
        
        // /Product/Details/Ergonomic%20Rubber%20Car
        // Mittels Mediator & CQRS
        [HttpGet()]
        public IActionResult Details(string id)
        {
            Product model = _mediator.Send(new GetProductByNameRequest(id)).Result;

            return View(model);
        }

        [HttpGet()]
        public IActionResult Expires()
        {
            IQueryable<Product> model = _mediator
                .Send(new GetByExpiryDateRequest(DateTime.Now.AddDays(14)))
                .Result;

            return View(model);
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
    }
}
