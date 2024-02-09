using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHU.Domain.Requests
{
    public class TransferRequestValidator : AbstractValidator<TransferRequest>
    {
        public TransferRequestValidator()
        {
            RuleFor(transfer => transfer.idContaOrigem)
                .NotEqual(Guid.Empty).WithMessage("O ID da conta de origem não pode ser vazio");

            RuleFor(transfer => transfer.idContaDestino)
                .NotEqual(Guid.Empty).WithMessage("O ID da conta de destino não pode ser vazio");

            RuleFor(transfer => transfer.valor)
                .GreaterThan(0).WithMessage("O valor da transferência deve ser maior que zero");
        }
    }
    public class TransferRequest
    {
        public Guid idContaOrigem { get; set; }
        public Guid idContaDestino { get; set; }
        public decimal valor { get; set; }
    }
}
