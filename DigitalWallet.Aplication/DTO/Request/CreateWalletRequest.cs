using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Aplication.DTO.Request
{
    public class CreateWalletRequest
    {
        public Guid UserId { get; set; }

        public decimal InitialBalance { get; set; }
    }
}
