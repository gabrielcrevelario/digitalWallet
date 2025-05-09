using DigitalWallet.Domain.entity;
using DigitalWallet.Domain.repository;

namespace DigitalWallet.Infrastructure.repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}