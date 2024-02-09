using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHU.Domain.DomainExceptions
{
    public class BankingOperationExceptions : Exception
    {
        public BankingOperationExceptions(string message) : base(message) { }
    }

    public class  InvalidValueException : BankingOperationExceptions
    {
        public InvalidValueException() : base("Valor inválido para a opção selecionada.") { }
    }
    public class InsufficientFundsException : BankingOperationExceptions
    {
        public InsufficientFundsException() : base("Saldo insuficiente.") { }
    }
    public class NonBusinessDayException : BankingOperationExceptions
    {
        public NonBusinessDayException() : base("Não é dia útil.") { }
    }
}
