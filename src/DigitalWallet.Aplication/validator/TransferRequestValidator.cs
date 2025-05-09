using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalWallet.Aplication.DTO.Request;
using FluentValidation;

namespace DigitalWallet.Aplication.Validator
{
    public class  TransferRequestValidator : AbstractValidator<TransferRequest>
{
    public TransferRequestValidator()
    {
        RuleFor(x => x.FromWalletId)
            .NotEmpty().WithMessage("Carteira de origem é obrigatória.");

        RuleFor(x => x.ToWalletId)
            .NotEmpty().WithMessage("Carteira de destino é obrigatória.")
            .NotEqual(x => x.FromWalletId).WithMessage("A carteira de origem e destino devem ser diferentes.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("O valor da transferência deve ser maior que zero.");
    }
}
}
