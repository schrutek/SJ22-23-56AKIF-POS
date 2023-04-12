using Spg.SpengerShop.Domain.Dtos;
using Spg.SpengerShop.Domain.Helpers;
using Spg.SpengerShop.Domain.Model;

namespace Spg.SpengerShop.Domain.Interfaces
{
    public interface IReadOnlyProductService
    {
        // Ausschließlich Read-Actions (GetById, GetAll, GetFiltered, GetByName, GetByEan)
        IQueryable<Product> Products { get; set; }
        IReadOnlyProductService Load();
        IEnumerable<Product> GetData();
    }
}
