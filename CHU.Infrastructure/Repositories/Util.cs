using CHU.Infrastructure.Interfaces;
using CHU.Infrastructure.Response;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CHU.Domain.DomainExceptions;
using System.Net.Http.Headers;
using CHU.Domain.Responses;

namespace CHU.Infrastructure.Repositories
{
    public class Util : IUtil
    {
        public List<Feriado> ListaFeriados = new List<Feriado>();
        public string url = string.Empty;
        public readonly IConfiguration _configuration;
        public DateTime dtUltVerificacao;
        public Util(IConfiguration configuration)
        {
            _configuration = configuration;
            url = _configuration["urlApi"];
        }
        public bool VerificaSeEDiaUtil(DateTime date)
        {
            if (ListaFeriados.Count == 0 || dtUltVerificacao < DateTime.Now.AddDays(-7))
                CarregaListaFeriados();
            return !(ListaFeriados.Any(x => x.Date == date) || DayOfWeek.Saturday == date.DayOfWeek 
                || DayOfWeek.Sunday == date.DayOfWeek);
        }
        private async void CarregaListaFeriados()
        {
            dtUltVerificacao = DateTime.Now;
            using (HttpClient http = new HttpClient())
            {
                string urlCompleta = $"{url}/{DateTime.Now.Year}";
                var resposta = await http.GetAsync(urlCompleta);
                if (resposta.IsSuccessStatusCode)
                {
                    string conteudo = await resposta.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<List<Feriado>>(conteudo);
                    if (obj != null)
                        ListaFeriados = obj;
                }
            }
        }
        public HandleException HandleException(Exception ex)
        {
            var erro = "Ocorreu um erro ao processar a requisição";
            int retorno = 500;

            switch (ex)
            {
                case BankingOperationExceptions _ when ex is BankingOperationExceptions:
                    retorno = 422;
                    erro = ex.Message;
                    break;

                case AccountExceptions _ when ex is AccountExceptions:
                    retorno = 404;
                    erro = ex.Message;
                    break;
                case FluentValidationException _ when ex is FluentValidationException:
                    retorno = 400;
                    erro = ex.Message;
                    break;
            }
            var corpoErro = new ErroResponse()
            {
                descricaoErro = erro
            };
            return new HandleException()
            {
                statusCode = retorno,
                erro = corpoErro
            };

        }
    }
}
