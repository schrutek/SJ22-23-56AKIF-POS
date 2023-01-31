using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Repository.Products
{
    public interface IProductRepository
    {
        IQueryable<Product> GetAll();
    }
}
