using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Domain.repository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();  
        Task<T> GetByIdAsync(Guid id);        
        Task AddAsync(T entity);             
        void UpdateAsync(T entity);               
        void DeleteAsync(T entity);               
        Task SaveAsync();                    
    }
}
