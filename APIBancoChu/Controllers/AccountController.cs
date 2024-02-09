using CHU.Domain.DomainExceptions;
using Microsoft.AspNetCore.Mvc;
using CHU.Service.Interfaces;
using CHU.Infrastructure.Interfaces;
using CHU.Domain.Requests;
using FluentValidation;
namespace APIBancoChu.Controllers
{
    [ApiController]
    [Route("v1/bank/api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _account;
        private readonly IUtil _util;
        private readonly IValidator<ExtratoRequest> _validatorExtrato;
        private readonly IValidator<AccountRequest> _validatorAccount;
        public AccountController(IAccountService account,IUtil util, IValidator<ExtratoRequest> validatorExtrato, IValidator<AccountRequest> validatorAccount)
        {
            _account = account;
            _util = util;
            _validatorExtrato = validatorExtrato;
            _validatorAccount = validatorAccount;
        }       

        [HttpGet]
        public async Task<IActionResult> BuscaExtrato([FromQuery] ExtratoRequest request)
        {            
            try
            {                
                return Ok(_account.GerarExtrato(request));
            }
            catch(Exception ex)
            {
                var retorno = _util.HandleException(ex);
                return StatusCode(retorno.statusCode, retorno.erro);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CriarUsuario([FromBody] AccountRequest request)
        {
            try
            {
                var id = _account.CriarConta(request);
                return Ok($"Usuário criado com sucesso.Id:{id}");
            }
            catch (Exception ex)
            {
                var retorno = _util.HandleException(ex);
                return StatusCode(retorno.statusCode, retorno.erro);
            }
        }
    }
}
