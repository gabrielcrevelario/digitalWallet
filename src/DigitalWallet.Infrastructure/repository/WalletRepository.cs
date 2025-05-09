using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DigitalWallet.Domain.entity;
using DigitalWallet.Domain.repository;
using DigitalWallet.Infrastructure.context;
using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.Infrastructure.repository
{
    public class WalletRepository : Repository<Wallet>, IWalletRepository
    {
        private readonly DigitalWalletContext _context;

        public WalletRepository(DigitalWalletContext context) : base(context)
        {
            _context = context;
        }




        public async Task<Wallet> GetByUserIdAsync(Guid userId) => await _context.Wallets
                .AsNoTracking()
                .FirstOrDefaultAsync(w => w.UserId == userId);

        public async Task<bool> HasSufficientBalance(Guid userId, decimal amount)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
            return wallet != null && wallet.Balance >= amount;
        }
    }
}
