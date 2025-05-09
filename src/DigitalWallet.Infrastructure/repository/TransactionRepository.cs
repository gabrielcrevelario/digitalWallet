using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalWallet.Domain.entity;
using DigitalWallet.Domain.repository;
using DigitalWallet.Infrastructure.context;
using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.Infrastructure.repository
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        private readonly DigitalWalletContext _context;

        public TransactionRepository(DigitalWalletContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetByUserAsync(Guid userId, DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue && startDate.Value.Kind == DateTimeKind.Unspecified)
                startDate = DateTime.SpecifyKind(startDate.Value, DateTimeKind.Utc);

            if (endDate.HasValue && endDate.Value.Kind == DateTimeKind.Unspecified)
                endDate = DateTime.SpecifyKind(endDate.Value, DateTimeKind.Utc);
            return await _context.Transactions
        .Where(t =>
            //(t.FromWallet.UserId == userId || t.ToWallet.UserId == userId) &&
            (!startDate.HasValue || t.CreatedAt >= startDate.Value) &&
            (!endDate.HasValue || t.CreatedAt <= endDate.Value)
        )
        .Include(t => t.FromWallet)
        .Include(t => t.ToWallet)
        .ToListAsync();
        }

        public async Task<bool> HasSufficientBalance(Guid walletId, decimal amount)
        {
            var wallet = await _context.Wallets
     .AsNoTracking()
     .FirstOrDefaultAsync(w => w.Id == walletId);

            return wallet != null && wallet.Balance >= amount;
        }
    }
}
