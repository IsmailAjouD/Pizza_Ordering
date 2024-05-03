using Microsoft.EntityFrameworkCore;
using Pizza_Ordaring.Infrastructure.Context;
using Pizza_Ordaring.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Pizza_Ordaring.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        private protected Pizza_OrdaringDbContext _dbContext;

        public GenericRepository(Pizza_OrdaringDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual T Add(T entity)
        {
            return _dbContext.Add(entity).Entity;
            
            //await _dbContext.Set<T>().AddAsync(entity);
            //await _dbContext.SaveChangesAsync();
            //return entity;
        }

   

        public virtual IEnumerable<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate).AsEnumerable();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> Remove(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return null;
            }
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public virtual async Task<T> Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
