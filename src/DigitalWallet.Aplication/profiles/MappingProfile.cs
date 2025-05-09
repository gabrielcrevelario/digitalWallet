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
            CreateMap<RegisterUserRequest, User>()
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash))
             .ForMember(dest => dest.Wallets, opt => opt.Ignore()); 

             CreateMap<LoginRequest, User>();
            CreateMap<User, UserResponse>(); 

            CreateMap<Wallet, WalletResponse>();
            CreateMap<TransferRequest, Transaction>();
            CreateMap<Transaction, TransactionResponse>();
        }
    }
}
