using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalWallet.Aplication.DTO.Request;
using DigitalWallet.Aplication.DTO.Response;
using DigitalWallet.Domain.entity;

namespace DigitalWallet.Aplication.interfaces
{
    public interface ITransactionService
    {
        Task<Transaction> TransferAsync(Transaction transaction);
        Task<TransactionResponse> CreateAsync(TransferRequest transferRequest);
        Task<TransactionResponse> GetByIdAsync(Guid id);
        Task<List<TransactionResponse>> GetUserTransactionsAsync(Guid userId, DateTime? startDate, DateTime? endDate);

    }
}
