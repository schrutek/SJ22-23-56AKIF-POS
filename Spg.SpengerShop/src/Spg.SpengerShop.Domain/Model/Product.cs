using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Spg.SpengerShop.Domain.Interfaces;

namespace Spg.SpengerShop.Domain.Model
{
    public class Product : IEnumerable<PropertyInfo>
    {
        public string Name { get; private set; } = string.Empty;
        public int Tax { get; set; } // Steuerklasse
        public string Ean { get; set; } = string.Empty;
        public string? Material { get; set; } = string.Empty;
        public DateTime? ExpiryDate { get; set; }

        public List<ShoppingCartItem> _shoppingCartItems = new();
        public virtual IReadOnlyList<ShoppingCartItem> ShoppingCartItems => _shoppingCartItems;

        protected List<Price> _prices = new();
        public virtual IReadOnlyList<Price> Prices => _prices;


        public int CategoryId { get; set; }
        public virtual Category CategoryNavigation { get; private set; } = default!;


        protected Product()
        { }
        public Product(string name, int tax, string ean, string? material, DateTime? expiryDate, Category category)
        {
            Name = name;
            Tax = tax;
            Ean = ean;
            Material = material;
            ExpiryDate = expiryDate;
            CategoryNavigation = category;
        }

        public IEnumerator<PropertyInfo> GetEnumerator()
        {
            return (IEnumerator<PropertyInfo>)this.GetType().GetProperties().AsEnumerable();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
