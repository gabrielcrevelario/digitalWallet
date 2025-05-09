using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalWallet.Domain.entity;
using DigitalWallet.Infrastructure.context;
namespace DigitalWallet.Infrastructure.data
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(DigitalWalletContext context)
        {
            if (context.Users.Any()) return; // Já populado

            var user1 = new User("Alice", "alice@email.com", "FeIUc0sDfMjh2MJz6N3kD9ssV8sUkXlM7VcuMvFMFgA=");
            var user2 = new User("Bob", "bob@email.com", "FeIUc0sDfMjh2MJz6N3kD9ssV8sUkXlM7VcuMvFMFgA=");
            user1.Id = Guid.NewGuid();
            user2.Id = Guid.NewGuid();

            var wallet1 = new Wallet(user1.Id, 1000m);
            var wallet2 = new Wallet(user2.Id, 500m);
            wallet1.Id = Guid.NewGuid();
            wallet2.Id = Guid.NewGuid();

            var transaction1 = new Transaction(wallet1.Id, wallet2.Id, 200m);
            var transaction2 = new Transaction(wallet2.Id, wallet1.Id, 50m);

            await context.Users.AddRangeAsync(user1, user2);
            await context.Wallets.AddRangeAsync(wallet1, wallet2);
            await context.Transactions.AddRangeAsync(transaction1, transaction2);

            await context.SaveChangesAsync();
        }
    }
}
