using CoreLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        //Private yerine Protected kullanmamızın nedeni; projenin ileriki dönemlerinde sınıflarımıza daha detaylı işlemler yapabilmemize olanak sağlamak içindir.
        //The reason we use Protected instead of Private is to allow us to perform more detailed operations on our classes in the later stages of the project.
        protected readonly Context _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(Context dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
           await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
           await _dbSet .AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await  _dbSet.AnyAsync(expression);
        }

        public async Task< IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
           return  await _dbSet.FindAsync(id);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
           _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }      
    }
}
