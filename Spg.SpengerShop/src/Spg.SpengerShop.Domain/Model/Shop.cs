using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Model
{
    public class Shop : EntityBase
    {
        protected Shop() { }
        public Shop(string companySuffix, string name, string location, string catchPhrase, string bs, Address address, Guid guid)
        {
            CompanySuffix = companySuffix;
            Name = name;
            Location = location;
            CatchPhrase = catchPhrase;
            Bs = bs;
            Address = address;
            Guid = guid;
        }

        public string CompanySuffix { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string CatchPhrase { get; set; }
        public string Bs { get; set; }
        public Guid Guid { get; private set; }
        public Address Address { get; set; }


        protected List<Category> _categories = new();
        public virtual IReadOnlyList<Category> Categories => _categories;
    }
}
