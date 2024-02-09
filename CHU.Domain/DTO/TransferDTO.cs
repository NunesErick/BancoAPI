using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHU.Domain.DTO
{
    public class TransferDTO
    {
        public Guid idTransferencia { get; set; }
        public Guid idContaOrigem { get; set; }
        public Guid idContaDestino { get; set; }
        public decimal valor { get; set; }
    }
}
