using System;
using Microsoft.EntityFrameworkCore;
using DigitalWallet.Domain.entity;

namespace DigitalWallet.Infrastructure.context
{
    public class DigitalWalletContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public DigitalWalletContext(DbContextOptions<DigitalWalletContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeamento da tabela Wallet
            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.ToTable("tbl_wallet");
                entity.Property(entity => entity.Id)
                .HasColumnName("id");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .IsRequired();

                entity.Property(e => e.Balance)
                    .HasColumnName("balance")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                // Relacionamento com o User
                entity.HasOne(e => e.User)
                    .WithMany(u => u.Wallets)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Configuração das transações de saída (Outcoming Transactions)
                entity.HasMany(e => e.OutgoingTransactions)
                    .WithOne(t => t.FromWallet)
                    .HasForeignKey(t => t.FromWalletId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Configuração das transações de entrada (Incoming Transactions)
                entity.HasMany(e => e.IncomingTransactions)
                    .WithOne(t => t.ToWallet)
                    .HasForeignKey(t => t.ToWalletId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Mapeamento da tabela User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("tbl_user");

                entity.Property(e => e.Id)
                .HasColumnName("id");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(e => e.PasswordHash)
                .HasMaxLength(16)
                .HasColumnName("password_hash")
                .IsRequired();
            });

            // Mapeamento da tabela Transaction
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("tbl_transaction");

                entity.Property(e => e.Id)
                .HasColumnName("id");
                entity.HasKey(t => t.Id);

                entity.Property(t => t.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(t => t.CreatedAt)
                    .HasColumnName("created_at")
                    .IsRequired();

                entity.Property(t => t.FromWalletId).HasColumnName("from_wallet_id").IsRequired();
                entity.HasOne(t => t.FromWallet)               
                    .WithMany(w => w.OutgoingTransactions)
                    .HasForeignKey(t => t.FromWalletId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(t => t.ToWalletId).HasColumnName("to_wallet_id").IsRequired();
                entity.HasOne(t => t.ToWallet)
                    .WithMany(w => w.IncomingTransactions)
                    .HasForeignKey(t => t.ToWalletId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}
