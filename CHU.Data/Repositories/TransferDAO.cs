using CHU.Data.Interface;
using CHU.Domain.DTO;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CHU.Infrastructure.CrossCutting.Enum;

namespace CHU.Data.Repositories
{
    public class TransferDAO : ITransferDAO
    {
        private readonly IConfiguration configuration;
        private string _conn;
        public TransferDAO(IConfiguration conf)
        {
            configuration = conf;
            _conn = configuration["ConnectionStrings:conn"];
        }
        public List<Extrato> ExtrairRelatorio(Guid id, DateTime dtInicio, DateTime dtFim)
        {
            List<Extrato> extrato = new List<Extrato>();
            using (IDbConnection conn = new SqlConnection(_conn))
            {
                string query = @"select d.dsDescricao as dsDestino ,t.valor * -1 as valor,t.dtTransf as dataRealizada from Contas c(nolock)
                            inner join Transferencias t (nolock) on t.idContaOrigem = c.id
                            inner join Contas d (nolock) on d.id = t.idContaDestino
                            where c.id = @id
                            and t.dtTransf between @dtinicio and @dtfim and t.situacao = @situacao
                            union
                            select d.dsDescricao as dsDestino ,t.valor,t.dtTransf as dataRealizada from Contas c(nolock)
                            inner join Transferencias t (nolock) on t.idContaOrigem = c.id
                            inner join Contas d (nolock) on d.id = t.idContaDestino
                            where d.id = @id
                            and t.dtTransf between @dtinicio and @dtfim and t.situacao = @situacao";
                var parametro = new
                {
                    Id = id,
                    dtinicio = dtInicio,
                    dtfim = dtFim,
                    situacao = (int)situacaoTransferencia.concluida
                };
                extrato = conn.Query<Extrato>(query, parametro).ToList();
            }
            return extrato;
        }

        public void IncluiErroTransferencia(TransferDTO transferencia, string erro)
        {
            using (IDbConnection conn = new SqlConnection(_conn))
            {
                string query = @"insert into Transferencias (idContaOrigem,idContaDestino,valor,situacao,erro,dtTransf)
                            values (@idContaOrigem,@idContaDestino,@valor,@situacao,@erro,@dtTransf)";
                var parametros = new
                {
                    valor = transferencia.valor,
                    idContaDestino = transferencia.idContaDestino,
                    idContaOrigem = transferencia.idContaOrigem,
                    situacao = (int)situacaoTransferencia.erro,
                    erro = erro,
                    dtTransf = DateTime.Now
                };
                conn.Execute(query, parametros);
            }
        }

        public void IncluiTransferencia(TransferDTO transferencia)
        {
            using (IDbConnection conn = new SqlConnection(_conn))
            {
                string query = @"insert into Transferencias (idContaOrigem,idContaDestino,valor,situacao,dtTransf)
                            values (@idContaOrigem,@idContaDestino,@valor,@situacao,@dtTransf)";
                var parametros = new
                {
                    valor = transferencia.valor,
                    idContaDestino = transferencia.idContaDestino,
                    idContaOrigem = transferencia.idContaOrigem,
                    situacao = (int)situacaoTransferencia.concluida,
                    dtTransf = DateTime.Now
                };
                conn.Execute(query, parametros);
            }
        }
    }
}
