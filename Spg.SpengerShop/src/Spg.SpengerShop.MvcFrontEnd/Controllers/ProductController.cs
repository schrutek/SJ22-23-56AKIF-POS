using Microsoft.AspNetCore.Mvc;
using Spg.SpengerShop.Domain.Dtos;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;

namespace Spg.SpengerShop.MvcFrontEnd.Controllers
{
    public class ProductController : Controller
    {
        private readonly IReadOnlyProductService _readOnlyProductService;
        private readonly IAddableProductService _addableProductService;

        public ProductController(IReadOnlyProductService readOnlyProductService, IAddableProductService addableProductService)
        {
            _readOnlyProductService = readOnlyProductService;
            _addableProductService = addableProductService;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> model = _readOnlyProductService.GetAll();
            return View(model);
        }

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
