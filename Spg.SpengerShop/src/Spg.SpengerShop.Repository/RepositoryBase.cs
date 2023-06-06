using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public int Create(TEntity newEntity)
        {
            try
            {
                DbSet<TEntity> dbSet = _db.Set<TEntity>();
                dbSet.Add(newEntity);
                return _db.SaveChanges(); // => Insert Into () Values ()
            }
            catch (DbUpdateException ex)
            {
                throw new ProductRepositoryCreateException("Create nicht möglich!", ex);
            }
        }

        public int Update(TEntity newEntity)
        {
            try
            {
                DbSet<TEntity> dbSet = _db.Set<TEntity>();
                dbSet.Update(newEntity);
                return _db.SaveChanges(); // => Update (x="", y="") where 
            }
            catch (DbUpdateException ex)
            {
                throw new ProductRepositoryCreateException("Create nicht möglich!", ex);
            }
        }

        public TEntity? GetByPK<TKey, TProperty>(
            TKey pk,
            Expression<Func<TEntity, IEnumerable<TProperty>>>? includeCollection = null,
            Expression<Func<TEntity, TProperty>>? includeReference = null)
            where TProperty : class
        {
            TEntity? entity = _db.Set<TEntity>().Find(pk);
            if (entity is not null)
            {
                if (includeCollection is not null)
                {
                    _db.Entry(entity).Collection(includeCollection).Load();
                }
                if (includeReference is not null)
                {
                    _db.Entry(entity).Reference(includeReference!).Load();
                }
            }
            return entity;
        }
        public TEntity? GetByPKAndIncudes<TKey, TProperty>(
            TKey pk, 
            List<Expression<Func<TEntity, IEnumerable<TProperty>>>?>? includeCollection = null,
            Expression<Func<TEntity, TProperty>>? includeReference = null)
            where TProperty : class
        {
            TEntity? entity = _db.Set<TEntity>().Find(pk);
            if (entity is not null)
            {
                if (includeCollection is not null)
                {
                    foreach (Expression<Func<TEntity, IEnumerable<TProperty>>>? item in includeCollection)
                    {
                        if (item is not null)
                        {
                            _db.Entry(entity).Collection(item).Load();
                        }
                    }
                }
                if (includeReference is not null)
                {
                    _db.Entry(entity).Reference(includeReference!).Load();
                }
            }
            return entity;
        }
        public TEntity? GetByPK<TKey1, TKey2>(TKey1 pk1, TKey2 pk2)
        {
            return _db.Set<TEntity>().Find(pk1, pk2);
        }
        public TEntity? GetByPK<TKey1, TKey2, TKey3>(TKey1 pk1, TKey2 pk2, TKey3 pk3)
        {
            return _db.Set<TEntity>().Find(pk1, pk2, pk3);
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
            //    throw new ...Exception("", ex);
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
