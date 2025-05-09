using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalWallet.Domain.entity;

namespace DigitalWallet.Domain.repository
{
    public interface IWalletRepository : IRepository<Wallet>
    {
        Task<Wallet> GetByUserIdAsync(Guid userId);
        Task<bool> HasSufficientBalance(Guid userId, decimal amount);
    }
}
