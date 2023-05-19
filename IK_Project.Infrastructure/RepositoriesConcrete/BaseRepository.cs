using IK_Project.Domain.Entities.Abstract;
using IK_Project.Domain.Enums;
using IK_Project.Domain.Repositories;
using IK_Project.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Infrastructure.RepositoriesConcrete
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        private readonly IKProjectDBContext _dbContext;
        protected DbSet<T> _table;
        public BaseRepository(IKProjectDBContext DbContext)
        {
            _dbContext = DbContext;
            _table = _dbContext.Set<T>();
        }
        public async Task Add(T item)
        {
            await _table.AddAsync(item);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<T>> GetDefault(Expression<Func<T, bool>> expression)
        {
            return await _table.Where(expression).ToListAsync();
        }
        public async Task<bool> Any(Expression<Func<T, bool>> expression)
        {
            return await _table.AnyAsync(expression);
        }
        public async Task Delete(T item)
        {
            item.Status = Status.Deleted;//Oluşturduğun entitynin status propertysinin değerini deleted yap
            await Update(item);
        }

        public async Task<T> GetBy(Expression<Func<T, bool>> expression)
        {
            return await _table.Where(expression).FirstAsync();
        }
        public async Task Update(T item)
        {
            //_table.Update(item);
            _dbContext.Entry<T>(item).State = EntityState.Modified;//Güncelleme Yap
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<T>> GetAll()
        {
            return await _table.ToListAsync();
        }
    }
}
