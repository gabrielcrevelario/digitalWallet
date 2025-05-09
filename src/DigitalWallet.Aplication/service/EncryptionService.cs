using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DigitalWallet.Aplication.interfaces;

namespace DigitalWallet.Aplication.service
{
    public class EncryptionService : IEncryptionService
    {
        private readonly string _key;
        public EncryptionService(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _key = configuration["Jwt:secretePart"]
                 ?? throw new ArgumentNullException("EncryptionKey", "A chave de criptografia não foi configurada.");
        }
        public string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_key);
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor();
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            // Combina IV + ciphertext
            var combined = aes.IV.Concat(cipherBytes).ToArray();

            return Convert.ToBase64String(combined);
        }

        public string Decrypt(string cipherText)
        {
            var combinedBytes = Convert.FromBase64String(cipherText);

            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_key);

            // Extrai IV (16 primeiros bytes) e o texto cifrado
            var iv = combinedBytes.Take(16).ToArray();
            var cipherBytes = combinedBytes.Skip(16).ToArray();

            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor();
            var plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}
