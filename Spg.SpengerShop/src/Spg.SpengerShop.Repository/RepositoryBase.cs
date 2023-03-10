using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Spg.SpengerShop.Domain.Exceptions;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IQueryable<TEntity> GetAll()
        {
            return _db.Set<TEntity>();
        }
    }
}
