using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Dtos
{
    public record ProductDto
    (
        string Name,
        int Tax,
        string Ean,
        string? Material,
        DateTime? ExpiryDate
    )
    {
        public List<ShoppingCartItemDto> ShoppingCartItems { get; init; } = new();
        public bool IsExpired => ExpiryDate < DateTime.Now;
        public int ShoppingCartItemsCount => ShoppingCartItems.Count;
    }
}
