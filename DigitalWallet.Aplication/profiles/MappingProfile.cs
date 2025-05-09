using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalWallet.Aplication.DTO.Request;
using DigitalWallet.Aplication.DTO.Response;
using DigitalWallet.Domain.entity;
using AutoMapper;

namespace DigitalWallet.Aplication.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            // Requests -> Entidades
            CreateMap<CreateWalletRequest, Wallet>()
                .ConstructUsing(r => new Wallet(r.UserId, r.InitialBalance));

            // Entidades -> Responses
            CreateMap<Wallet, WalletResponse>();
            CreateMap<TransferRequest, Transaction>();
            CreateMap<Transaction, TransactionResponse>();
            CreateMap<RegisterUserRequest, User>();
            CreateMap<User, UserResponse>();
        }
    }
}
