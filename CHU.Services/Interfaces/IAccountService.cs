using CHU.Domain.DTO;
using CHU.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHU.Service.Interfaces
{
    public interface IAccountService
    {
        void Transferir(TransferRequest transferencia);
        ExtratoResponseDTO GerarExtrato(ExtratoRequest request);
        Guid CriarConta(AccountRequest conta);
    }
}
