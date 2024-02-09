using CHU.Domain.DTO;
using CHU.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CHU.Data.Interface
{
    public interface IAccountDAO
    {        
        AccountDTO BuscaConta(Guid id);
        void InserirValorConta(Guid idConta, decimal valor);
        Guid CriaConta(AccountRequest novaConta);
    }
}
