using ContaCorrente.Dominio.DTO;
using ContaCorrente.Repositorio.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ContaCorrente.Dominio.Mapper
{
    public static class ContaMapper
    {
        public static ContaDTO ConvertToDTO(this Conta conta, Pessoa pessoa, IList<Transacao> transacoes)
        {
            return new ContaDTO
            {
                IdConta = conta.IdConta,
                IdPessoa = conta.IdPessoa,
                SaldoAtual = conta.SaldoAtual,
                Pessoa = new PessoaDTO
                {
                    IdPessoa = pessoa.IdPessoa,
                    CPF = pessoa.Cpf,
                    Nome = pessoa.Nome
                },
                Transacoes = transacoes.Select(x =>
                    new TransacaoDTO
                    {
                        IdTransacao = x.IdTransacao,
                        IdConta = x.IdConta,
                        IdTipoTransacao = x.IdTipoTransacao,
                        DataHora = x.DataHora,
                        Valor = x.Valor,
                        Historico = x.Historico,
                        TipoTransacao = new TipoTransacaoDTO
                        {
                            Nome = x.IdTipoTransacaoNavigation.Nome,
                            Descricao = x.IdTipoTransacaoNavigation.Descricao,
                            DescricaoAbreviada = x.IdTipoTransacaoNavigation.DescricaoAbreviada,
                            FlagCredito = x.IdTipoTransacaoNavigation.FlagCredito,
                            FlagSaldoAtual = x.IdTipoTransacaoNavigation.FlagSaldoAtual,
                            IdTipoTransacao = x.IdTipoTransacaoNavigation.IdTipoTransacao
                        }

                    }).ToList()
            };
        }

    }
}
