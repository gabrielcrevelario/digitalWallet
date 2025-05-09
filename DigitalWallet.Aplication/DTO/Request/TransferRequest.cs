using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Aplication.DTO.Request
{
    public class TransferRequest
    {
        public Guid FromWalletId { get; set; }

        public Guid ToWalletId { get; set; }

        public decimal Amount { get; set; }
    }
}
