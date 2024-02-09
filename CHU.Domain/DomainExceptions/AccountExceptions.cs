using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHU.Domain.DomainExceptions
{
    public class AccountExceptions : Exception
    {
        public AccountExceptions(string message) : base(message) { }
    }
    public class AccountNotFoundException : AccountExceptions
    {
        public AccountNotFoundException(): base("Conta não encontrada na base de dados.") { }        
    }
    public class InactiveAccountException : AccountExceptions
    {
        public InactiveAccountException() : base("Conta inativada.") { }
    }
}
