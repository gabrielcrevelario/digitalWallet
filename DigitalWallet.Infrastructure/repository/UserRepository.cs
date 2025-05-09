using DigitalWallet.Domain.entity;
using DigitalWallet.Infrastructure.context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DigitalWallet.Infrastructure.repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DigitalWalletContext _context;

        public UserRepository(DigitalWalletContext context) : base(context)
        {
            _context = context; 
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(f => f.Email == email);
        }
    }
}
