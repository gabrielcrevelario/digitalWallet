using DigitalWallet.Domain.entity;

public class Wallet
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal Balance { get; set; }

    public User User { get; private set; }
    public ICollection<Transaction> OutgoingTransactions { get; set; } = new List<Transaction>();
    public ICollection<Transaction> IncomingTransactions { get; set; } = new List<Transaction>();

    // 🔧 Construtor sem parâmetros necessário para o EF
    public Wallet() { }

    // Construtor de negócio (usado na criação via código)
    public Wallet(Guid userId, decimal initialBalance)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("UserId inválido.", nameof(userId));

        if (initialBalance < 0)
            throw new ArgumentException("Saldo inicial não pode ser negativo.", nameof(initialBalance));

        Id = Guid.NewGuid();
        UserId = userId;
        Balance = initialBalance;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive.");
        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive.");
        if (Balance < amount)
            throw new InvalidOperationException("Insufficient funds.");
        Balance -= amount;
    }

    public void TransferTo(Wallet destination, decimal amount)
    {
        if (destination == null) throw new ArgumentNullException(nameof(destination));
        Withdraw(amount);
        destination.Deposit(amount);
    }
}
