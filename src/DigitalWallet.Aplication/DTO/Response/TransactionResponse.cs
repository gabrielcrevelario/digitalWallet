using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Aplication.DTO.Response
{
    public class TransactionResponse
    {
        public Guid Id { get; set; }

        public Guid FromWalletId { get; set; }
        public Guid FromUserId { get; set; }
        public Guid ToWalletId { get; set; }
        public Guid ToUserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
