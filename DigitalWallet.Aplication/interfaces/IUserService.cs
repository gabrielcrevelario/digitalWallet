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
    public interface IUserService
    {
        Task<UserResponse> CreateUserAsync(RegisterUserRequest registerUserRequest);
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> GetUserByEmailAsync(string email);
    }
}
