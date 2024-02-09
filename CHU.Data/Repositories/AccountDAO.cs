using CHU.Data.Interface;
using CHU.Domain.DomainExceptions;
using CHU.Domain.DTO;
using CHU.Domain.Requests;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHU.Data.Repositories
{
    public class AccountDAO : IAccountDAO
    {
        private readonly IConfiguration configuration;
        private string _conn;
        public AccountDAO(IConfiguration conf)
        {
            configuration= conf;
            _conn = configuration["ConnectionStrings:conn"];
        }
        public AccountDTO? BuscaConta(Guid id)
        {
            AccountDTO? retorno =  new AccountDTO();
            using (IDbConnection conn = new SqlConnection(_conn))
            {
                string query = "select * from Contas (nolock) where Id = @Id";
                var parametro = new
                {
                    Id = id
                };
                retorno = conn.Query<AccountDTO>(query, parametro).FirstOrDefault();
            }
            return retorno;
        }

        public Guid CriaConta(AccountRequest novaConta)
        {
            Guid retorno = new Guid();
            using (IDbConnection conn = new SqlConnection(_conn))
            {
                string query = @"INSERT INTO Contas (saldo, chequeEspecial, situacao, dsDescricao, dtCriacao)
                                OUTPUT INSERTED.id
                                VALUES (@saldo, @chequeEspecial, @situacao, @dsDescricao, GETDATE())";
                retorno = conn.QueryFirstOrDefault<Guid>(query, novaConta);
            }
            return retorno;
        }

        public void InserirValorConta(Guid idConta, decimal valor)
        {
            var conta = BuscaConta(idConta);
            conta.saldo +=valor;
            using (IDbConnection conn = new SqlConnection(_conn))
            {
                string query = @"update Contas set saldo = @saldo
                                 where Id = @Id";
                var parametro = new
                {
                    Id = idConta,
                    saldo = conta.saldo
                };
                conn.Execute(query,parametro);
            }
        }       
    }
}
