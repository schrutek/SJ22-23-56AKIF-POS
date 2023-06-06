using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Model
{
    public class ShippableShoppingCartItem : ShoppingCartItem
    {
        public Address Address { get; set; } = default!;

        protected ShippableShoppingCartItem()
        { }
        public ShippableShoppingCartItem(ShoppingCart shoppingCartNavigation, Product productNavigation, Address address)
            : base(shoppingCartNavigation, productNavigation)
        {
            Address = address;
        }
    }
}
