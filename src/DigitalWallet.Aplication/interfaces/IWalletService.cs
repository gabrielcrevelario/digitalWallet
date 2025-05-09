using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalWallet.Aplication.DTO.Request;
using DigitalWallet.Domain.entity;

namespace DigitalWallet.Aplication.interfaces
{
    public interface IWalletService
    {
        Task<Wallet> GetWalletByUserIdAsync(Guid userId);
        Task DepositAsync(Guid walletId, decimal amount);
        Task WithdrawAsync(Guid walletId, decimal amount);
        Task TransferAsync(TransferRequest request);
    }
}
