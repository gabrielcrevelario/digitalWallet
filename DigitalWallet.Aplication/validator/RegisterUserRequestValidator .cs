using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalWallet.Aplication.DTO.Request;
using FluentValidation;

namespace DigitalWallet.Aplication.Validator
{
    public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username é obrigatório.")
                .MinimumLength(3).WithMessage("Username deve ter no mínimo 3 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email é obrigatório.")
                .EmailAddress().WithMessage("Email inválido.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Senha é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve ter no mínimo 6 caracteres.");
        }
    }
}
