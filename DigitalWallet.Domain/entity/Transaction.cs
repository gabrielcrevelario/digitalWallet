namespace DigitalWallet.Domain.entity
{
    public class Transaction
    {
        public Guid Id { get; private set; }
        public Guid FromWalletId { get; private set; }
        public Guid ToWalletId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Wallet FromWallet { get; private set; }
        public Wallet ToWallet { get; private set; }

        public Transaction(Guid fromWalletId, Guid toWalletId, decimal amount)
        {
            if (fromWalletId == toWalletId)
                throw new ArgumentException("A transfer�ncia n�o pode ocorrer para a mesma carteira.");

            if (amount <= 0)
                throw new ArgumentException("O valor da transa��o deve ser maior que zero.");

            Id = Guid.NewGuid();
            FromWalletId = fromWalletId;
            ToWalletId = toWalletId;
            Amount = amount;
            CreatedAt = DateTime.UtcNow;
        }
    }
}