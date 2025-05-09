namespace DigitalWallet.Domain.entity
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get;  set; }
        public string Email { get; set; }
        public string PasswordHash { get;  set; }
        public List<Wallet> Wallets { get; set; }

        public User(string name, string email, string passwordHash)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}