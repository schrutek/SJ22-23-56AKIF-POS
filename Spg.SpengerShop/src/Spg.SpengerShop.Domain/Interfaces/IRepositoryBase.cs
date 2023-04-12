using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Interfaces
{
    public interface IRepositoryBase<TEntity>
        where TEntity : class
    {
        int Create(TEntity newProduct);
        int Update(TEntity newEntity);
    }
}
