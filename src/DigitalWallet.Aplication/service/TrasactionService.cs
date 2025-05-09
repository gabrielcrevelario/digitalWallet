using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalWallet.Domain.entity;
using DigitalWallet.Domain.repository;
using DigitalWallet.Aplication.interfaces;
using DigitalWallet.Aplication.DTO.Response;
using DigitalWallet.Aplication.DTO.Request;
using AutoMapper;
using FluentValidation;

namespace DigitalWallet.Aplication.service
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IRepository<Wallet> _walletRepository;
        private readonly IValidator<TransferRequest> _validator;
        private readonly IMapper _mapper;
        public TransactionService(ITransactionRepository transactionRepository, IRepository<Wallet> walletRepository, IMapper mapper, IValidator<TransferRequest> validator)
        {
            _transactionRepository = transactionRepository;
            _walletRepository = walletRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Transaction> TransferAsync(Transaction transaction)
        {
            var fromWallet = await _walletRepository.FirstOrDefaultAsync(f => f.Id == transaction.FromWalletId);
            var toWallet = await _walletRepository.FirstOrDefaultAsync(f => f.Id == transaction.ToWalletId);

            if (fromWallet == null || toWallet == null)
                throw new ArgumentException("Invalid wallets");

            await _transactionRepository.AddAsync(transaction);
            return transaction;
        }
        public async Task<List<TransactionResponse>> GetUserTransactionsAsync(Guid userId, DateTime? startDate, DateTime? endDate)
        {
            var transactions = await _transactionRepository.GetByUserAsync(userId, startDate, endDate);
            var response = _mapper.Map<List<TransactionResponse>>(transactions);
            return response;
        }

        public async Task<TransactionResponse> CreateAsync(TransferRequest transferRequest)
        {
            await _validator.ValidateAndThrowAsync(transferRequest);
            var transaction = _mapper.Map<Transaction>(transferRequest);
            await _transactionRepository.AddAsync(transaction);
            return _mapper.Map<TransactionResponse>(transaction);
        }

        public async Task<TransactionResponse> GetByIdAsync(Guid id)
        {
            var transaction = await _transactionRepository.FirstOrDefaultAsync(f => f.Id == id);
            var transactionResponse = _mapper.Map<TransactionResponse>(transaction);
            return transactionResponse;
        }

     
    }
}
