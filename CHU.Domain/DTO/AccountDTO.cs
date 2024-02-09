using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CHU.Domain.DTO
{
    public class AccountDTO
    {
        public Guid id { get; set; }
        public decimal chequeEspecial { get; set; }
        public decimal saldo { get; set; }
        public bool situacao { get; set; }
        public string dsDescricao { get; set; }
    }
}
