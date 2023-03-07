using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Interfaces
{
    public interface IReadOnlyRepositoryBase<TEntity>
        where TEntity : class
    {
        TEntity? GetByPK<TKey>(TKey pk);

        T? GetByGuid<T>(Guid guid) where T : class, IFindableByGuid;

        IQueryable<TEntity> GetAll();

        // GetFiltered
        // GetByGuid
        // GetBy...
    }
}
