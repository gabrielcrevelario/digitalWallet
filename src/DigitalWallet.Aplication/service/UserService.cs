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
using DigitalWallet.Infrastructure.repository;
using FluentValidation;

namespace DigitalWallet.Aplication.service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IValidator<RegisterUserRequest> _userValidator;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IValidator<RegisterUserRequest> userValidator, IMapper mapper, IEncryptionService encryptionService)
        {
            _userRepository = userRepository;
            _userValidator = userValidator;
            _encryptionService = encryptionService;
            _mapper = mapper;
        }

        public async Task<UserResponse> CreateUserAsync(RegisterUserRequest registerUserRequest)
        {
            var validation = _userValidator.Validate(registerUserRequest);   
            if(validation.IsValid)
            {

                var user = _mapper.Map<User>(registerUserRequest);
                user.PasswordHash = _encryptionService.Encrypt(user.PasswordHash);
                await _userRepository.AddAsync(user);
                return _mapper.Map<UserResponse>(user);
            } throw new Exception(validation.Errors.FirstOrDefault().ErrorMessage);
        }


        public async Task<User> GetUserByEmailAsync(String email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _userRepository.FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}