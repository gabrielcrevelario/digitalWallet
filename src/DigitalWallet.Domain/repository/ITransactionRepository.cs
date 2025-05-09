using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalWallet.Domain.entity;

namespace DigitalWallet.Domain.repository
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<List<Transaction>> GetByUserAsync(Guid userId, DateTime? startDate, DateTime? endDate);
        Task<bool> HasSufficientBalance(Guid userId, decimal amount);
    }
}
