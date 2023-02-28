using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Domain.Exceptions;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IQueryable<TEntity> GetAll()
        {
            return _db.Set<TEntity>();
        }
    }
}
