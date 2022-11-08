using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Model
{
    public enum Genders 
    { 
        Male = 0, 
        Female = 1, 
        Other = 2 
    }

    public class Customer
    {
        public Genders Gender { get; set; }
        public string CustomerNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EMail { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Address { get; set; } = string.Empty;
        public DateTime RegistrationDateTime { get; set; }

        public List<ShoppingCart> ShoppingCarts { get; set; } = new();
    }
}
