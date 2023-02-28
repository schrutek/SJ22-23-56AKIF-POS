using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Repository
{
    public interface IReadOnlyRepositoryBase<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> GetAll();
    }
}
