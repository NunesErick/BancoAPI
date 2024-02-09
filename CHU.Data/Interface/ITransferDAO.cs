using CHU.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHU.Data.Interface
{
    public interface ITransferDAO
    {
        void IncluiTransferencia(TransferDTO transferencia);
        void IncluiErroTransferencia(TransferDTO trans, string msgErro);
        List<Extrato> ExtrairRelatorio(Guid id, DateTime dtInicio, DateTime dtFim);
    }
}
