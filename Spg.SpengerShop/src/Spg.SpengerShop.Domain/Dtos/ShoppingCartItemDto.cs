using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Dtos
{
    public record ShoppingCartItemDto
    (
        int Id,
        bool IsShippable
    )
    {
        public ShoppingCartDto ShoppingCartNavigation { get; init; } = default!;
        public ProductDto ProductNavigation { get; init; } = default!;
    }
}
