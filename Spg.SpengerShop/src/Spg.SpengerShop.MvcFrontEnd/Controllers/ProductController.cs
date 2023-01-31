using Microsoft.AspNetCore.Mvc;
using Spg.SpengerShop.Application.Products;
using Spg.SpengerShop.Domain.Model;

namespace Spg.SpengerShop.MvcFrontEnd.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> model = _productService.GetAll("A");
            return View(model);
        }
    }
}
