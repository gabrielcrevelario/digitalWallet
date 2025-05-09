using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace DigitalWallet.Aplication.Validator
{
    public class RequestValidator
    {
        public static async Task ValidateAsync<T>(T request, IValidator<T> validator)
        {
            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
        }
    }
}
