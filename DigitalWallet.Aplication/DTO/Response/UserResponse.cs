using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalWallet.Domain.entity;

namespace DigitalWallet.Aplication.DTO.Response
{
    public class UserResponse
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public String PasswordHash { get; private set; }

    }
}
