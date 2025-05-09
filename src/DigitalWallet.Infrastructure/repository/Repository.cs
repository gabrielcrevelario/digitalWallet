using Microsoft.EntityFrameworkCore;
using DigitalWallet.Infrastructure.context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalWallet.Domain.repository;
using System.Linq.Expressions;

namespace DigitalWallet.Infrastructure.repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DigitalWalletContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DigitalWalletContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }

        public void DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

      
    }
}
