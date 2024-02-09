using FluentValidation;
using System.Security.Principal;

namespace CHU.Domain.Requests
{
    public class AccountRequestValidator : AbstractValidator<AccountRequest>
    {
        public AccountRequestValidator()
        {
            RuleFor(account => account.chequeEspecial)
                .GreaterThanOrEqualTo(0).WithMessage("O limite do cheque especial deve ser maior ou igual a zero");

            RuleFor(account => account.saldo)
                .GreaterThanOrEqualTo(0).WithMessage("O saldo deve ser maior ou igual a zero");

            RuleFor(account => account.situacao)
                .NotNull().WithMessage("O status de ativação não pode ser nulo");
            RuleFor(account => account.dsDescricao)
                .NotNull().WithMessage("A descrição não pode ser nula");
        }
    }
    public class AccountRequest
    {
        public decimal chequeEspecial { get; set; }
        public decimal saldo { get; set; }
        public bool situacao { get; set; }
        public string dsDescricao { get; set; }
    }
}
