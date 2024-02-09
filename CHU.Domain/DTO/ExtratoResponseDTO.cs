using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHU.Domain.DTO
{
    public class ExtratoResponseDTO
    {
        public Guid idConta { get; set; }
        public decimal saldo { get; set; }
        public List<Extrato> extrato { get; set; }
    }

    public class Extrato
    {
        public string dsDestino { get; set; }
        public DateTime dataRealizada { get; set; }
        public decimal valor { get; set; }
    }

}
