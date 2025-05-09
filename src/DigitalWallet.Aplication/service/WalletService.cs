using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DigitalWallet.Aplication.DTO.Request;
using DigitalWallet.Aplication.DTO.Response;
using DigitalWallet.Aplication.interfaces;
using DigitalWallet.Aplication.Validator;
using DigitalWallet.Domain.entity;
using DigitalWallet.Domain.repository;
using DigitalWallet.Domain.service;
using FluentValidation;

namespace DigitalWallet.Aplication.service
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IValidator<CreateWalletRequest> _createWalletValidator;
        private readonly IValidator<TransferRequest> _transferValidator;
        private readonly IMapper _mapper;
        public WalletService(IWalletRepository walletRepository,
                    IValidator<CreateWalletRequest> createWalletValidator, IValidator<TransferRequest> transferValidator, IMapper mapper
            )
        {
            _walletRepository = walletRepository;
            _createWalletValidator = createWalletValidator;
            _transferValidator = transferValidator;
            _mapper = mapper;
        }

        public async Task<Wallet> GetWalletByUserIdAsync(Guid userId)
        {
            return await _walletRepository.GetByUserIdAsync(userId);
        }
        public async Task<WalletResponse> CreateAsync(CreateWalletRequest request)
        {
            await RequestValidator.ValidateAsync(request, _createWalletValidator);

            var wallet = _mapper.Map<Wallet>(request);
            await _walletRepository.AddAsync(wallet);

            return _mapper.Map<WalletResponse>(wallet);
        }

        public async Task TransferAsync(TransferRequest request)
        {
            var validation = await _transferValidator.ValidateAsync(request);
            if (!validation.IsValid)
                throw new ValidationException(validation.Errors);

            // restante da lógica da transferência...
        }

        public async Task DepositAsync(Guid walletId, decimal amount)
        {
            var wallet = await _walletRepository.GetByIdAsync(walletId);
            if (wallet != null)
            {
                wallet.Deposit(amount);
                _walletRepository.UpdateAsync(wallet);
            }
        }

        public async Task WithdrawAsync(Guid walletId, decimal amount)
        {
            var wallet = await _walletRepository.GetByIdAsync(walletId);
            if (wallet != null)
            {
                wallet.Withdraw(amount);
                _walletRepository.UpdateAsync(wallet);
            }
        }
    }
}
