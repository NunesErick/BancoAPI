using CHU.Data.Interface;
using CHU.Domain.DomainExceptions;
using CHU.Domain.DTO;
using CHU.Domain.Requests;
using CHU.Infrastructure.Interfaces;
using CHU.Service.Interfaces;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CHU.Service.Repositories
{
    public class AccountService : IAccountService
    {
        private readonly IAccountDAO _accountDAO;
        private readonly ITransferDAO _transferDAO;
        private readonly IUtil _util;
        public AccountService(IAccountDAO accountDAO, ITransferDAO transferDAO, IUtil util)
        {
            _accountDAO = accountDAO;
            _transferDAO = transferDAO;
            _util = util;
        }

        public Guid CriarConta(AccountRequest conta)
        {            
            return _accountDAO.CriaConta(conta);
        }

        public ExtratoResponseDTO GerarExtrato(ExtratoRequest request)
        {
            if (request.DtInicial== null) request.DtInicial = DateTime.Now.AddDays(-30);
            if (request.DtFinal == null) request.DtFinal = DateTime.Now;
            var conta = _accountDAO.BuscaConta(request.Id);
            VerificaDisponibilidadeDaConta(conta);
            var retorno = new ExtratoResponseDTO()
            {
                idConta = conta.id,
                saldo = conta.saldo,
                extrato = _transferDAO.ExtrairRelatorio(request.Id, (DateTime)request.DtInicial, (DateTime)request.DtFinal)
            };
            return retorno;
        }

        public void Transferir(TransferRequest transferencia)
        {
            var transfer = new TransferDTO()
            {
                idContaDestino = transferencia.idContaDestino,
                idContaOrigem = transferencia.idContaOrigem,
                valor = transferencia.valor
            };
            try
            {                
                if (!_util.VerificaSeEDiaUtil(DateTime.Now)) throw new NonBusinessDayException();
                var contaOrigem = _accountDAO.BuscaConta(transferencia.idContaOrigem);
                VerificaDisponibilidadeDaConta(contaOrigem);
                var contaDestino = _accountDAO.BuscaConta(transferencia.idContaDestino);
                VerificaDisponibilidadeDaConta(contaDestino);
                if (contaOrigem.saldo + contaOrigem.chequeEspecial + transferencia.valor * -1 < 0) throw new InsufficientFundsException();                
                using (var scope = new TransactionScope())
                {
                    _accountDAO.InserirValorConta(transfer.idContaOrigem, transferencia.valor * -1);
                    _accountDAO.InserirValorConta(transfer.idContaDestino, transfer.valor);
                    _transferDAO.IncluiTransferencia(transfer);
                    scope.Complete();
                }
            }
            catch(Exception ex)
            {
                _transferDAO.IncluiErroTransferencia(transfer, ex.Message);
                throw ex;
            }            
        }
        private void VerificaDisponibilidadeDaConta(AccountDTO conta)
        {
            if (conta == null) throw new AccountNotFoundException();
            if (!conta.situacao) throw new InactiveAccountException();
        }
    }
}
