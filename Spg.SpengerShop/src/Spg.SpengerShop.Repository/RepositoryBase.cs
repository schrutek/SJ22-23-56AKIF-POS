﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Spg.SpengerShop.Domain.Exceptions;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>, IReadOnlyRepositoryBase<TEntity>
        where TEntity : class
    {
        private readonly SpengerShopContext _db;

        public RepositoryBase(SpengerShopContext db)
        {
            _db = db;
        }

        public void Create(TEntity newEntity)
        {
            try
            {
                DbSet<TEntity> dbSet = _db.Set<TEntity>();
                dbSet.Add(newEntity);
                _db.SaveChanges(); // => Insert
            }
            catch (DbUpdateException ex)
            {
                throw new ProductRepositoryCreateException("Create nicht möglich!", ex);
            }
        }

        public TEntity? GetByPK<TKey>(TKey pk)
        {
            return _db.Set<TEntity>().Find(pk);
        }

        public T? GetByGuid<T>(Guid guid) where T : class, IFindableByGuid
        {
            return _db.Set<T>().SingleOrDefault(e => e.Guid == guid);

            // Darf man natürlich auch so machen
            //try
            //{
            //    return _db.Set<T>().SingleOrDefault(e => e.Guid == guid);
            //}
            //catch (InvalidOperationException ex)
            //{
            //    throw new Exception("", ex);
            //}
        }

        public T? GetByEMail<T>(string eMail) where T : class, IFindableByEMail
        {
            return _db.Set<T>().SingleOrDefault(e => e.EMail == eMail);
        }

        private IQueryable<TEntity> GetQueryable(
            Expression<Func<TEntity, bool>>? filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? sortOrder,
            string? includeNavigationProperty = null,
            int? skip = null,
            int? take = null)
        {
            IQueryable<TEntity> result = _db.Set<TEntity>();

            if (filter != null)
            {
                result = result.Where(filter);
            }
            if (sortOrder != null)
            {
                result = sortOrder(result);
            }

            includeNavigationProperty = includeNavigationProperty ?? String.Empty;
            foreach (var item in includeNavigationProperty.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                result = result.Include(item);
            }

            int count = result.Count();
            if (skip.HasValue)
            {
                result = result.Skip(skip.Value);
            }
            if (take.HasValue)
            {
                result = result.Take(take.Value);
            }
            return result;
        }

        public IQueryable<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeNavigationProperty = "",
            int? skip = null,
            int? take = null)
        {
            return GetQueryable(
                null,
                orderBy,
                includeNavigationProperty,
                skip,
                take
            );
        }

        public IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeNavigationProperty = "",
            int? skip = null,
            int? take = null)
        {
            return GetQueryable(
                filter,
                orderBy,
                includeNavigationProperty,
                skip,
                take
            );
        }
    }
}
