using CHU.Domain.DomainExceptions;
using CHU.Domain.DTO;
using CHU.Domain.Requests;
using CHU.Infrastructure.Interfaces;
using CHU.Service.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace APIBancoChu.Controllers
{
    [ApiController]
    [Route("v1/bank/api/bankingoperations")]
    public class BankingOperationsController : ControllerBase
    {
        private readonly IAccountService _account;
        private readonly IUtil _util;
        private readonly IValidator<TransferRequest> _validatorTransfer;
        public BankingOperationsController(IAccountService account, IUtil util, IValidator<TransferRequest> validatorTransfer)
        {
            _account = account;
            _util = util;
            _validatorTransfer = validatorTransfer;
        }
        [HttpPost("transfer")]
        public async Task<IActionResult> RealizarTransferencia([FromBody] TransferRequest transfer)
        {
            try
            {
                _account.Transferir(transfer);
            }
            catch (Exception ex)
            {
                var retorno = _util.HandleException(ex);
                return StatusCode(retorno.statusCode, retorno.erro);
            }
            return Ok();
        }
    }
}
