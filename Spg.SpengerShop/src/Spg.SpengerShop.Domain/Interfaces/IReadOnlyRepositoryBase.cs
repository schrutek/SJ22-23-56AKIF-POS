﻿using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Interfaces
{
    public interface IReadOnlyRepositoryBase<TEntity>
        where TEntity : class
    {
        TEntity? GetByPK<TKey>(TKey pk);

        T? GetByGuid<T>(Guid guid) where T : class, IFindableByGuid;

        IQueryable<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeNavigationProperty = "",
            int? skip = null,
            int? take = null);

        IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeNavigationProperty = "",
            int? skip = null,
            int? take = null);
        }
}
