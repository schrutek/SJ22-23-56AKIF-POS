using Spg.SpengerShop.Domain.Dtos;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;

namespace Spg.SpengerShop.ServicesExtensions
{
    public static class ProductServiceExtension
    {

        public static IReadOnlyProductService UseFilterContainsName(this IReadOnlyProductService service, string filter)
        {
            // Filterlogik
            if (!string.IsNullOrEmpty(filter))
            {
                service.Products = service
                    .Products
                    .Where(p => p.Name.ToLower().Contains(filter.ToLower()));
            }
            return service;
        }

        public static IReadOnlyProductService UseFilterByExpiryDate(this IReadOnlyProductService service, DateTime from, DateTime to)
        {
            // Filterlogik
            service.Products = service.Products.Where(p => p.ExpiryDate > from && p.ExpiryDate <= to);
            return service;
        }

        public static IReadOnlyProductService UseSorting(this IReadOnlyProductService service, string columName) // "tax" oder "tax_desc"
        {
            // Sortierlogik
            // switch case (Delegate) / Reflextions
            service.Products = service.Products.OrderBy(p => p.GetType().GetProperties().SingleOrDefault(p => p.Name.ToLower() == columName.ToLower()));
            service.Products = service.Products.OrderByDescending(p => p.Tax);
            return service;
        }

        public static IReadOnlyProductService UsePaging(this IReadOnlyProductService service, int pageIndex, int pageSize)
        {
            // TODO: Implementation
            // Skip/Take
            return service;
        }
    }
}