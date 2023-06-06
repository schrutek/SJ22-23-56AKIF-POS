using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Model
{
    public class ShoppingCartItem : EntityBase
    {
        public int ShoppingCartNavigationId { get; set; }
        public virtual ShoppingCart ShoppingCartNavigation { get; set; } = default!;
        public string ProductNavigationName { get; set; }
        public virtual Product ProductNavigation { get; set; } = default!;
        public string ItemType { get; set; } = string.Empty;

        protected ShoppingCartItem()
        { }

        public ShoppingCartItem(ShoppingCart shoppingCartNavigation, Product productNavigation)
        {
            ShoppingCartNavigation = shoppingCartNavigation;
            ProductNavigation= productNavigation;
        }
    }
}
