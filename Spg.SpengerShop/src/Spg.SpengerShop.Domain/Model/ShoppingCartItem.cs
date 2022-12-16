using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Model
{
    public class ShoppingCartItem
    {
        public int Id { get; private set; }

        public int ShoppingCartNavigationId { get; set; }
        public ShoppingCart ShoppingCartNavigation { get; set; } = default!;
        public int ProductNavigationId { get; set; }
        public Product ProductNavigation { get; set; } = default!;

        protected ShoppingCartItem()
        { }
    }
}
