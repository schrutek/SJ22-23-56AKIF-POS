using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Model
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int ItemsCount { get; }

        public Customer CustomerNavigation { get; set; } = default!;
        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = new();
    }
}
