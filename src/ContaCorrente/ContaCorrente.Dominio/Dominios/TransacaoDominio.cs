using ContaCorrente.Commum;
using ContaCorrente.Dominio.DTO;
using ContaCorrente.Dominio.Interfaces;
using ContaCorrente.Repositorio.Entities;
using ContaCorrente.Repositorio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContaCorrente.Dominio.Dominios
{
    public class TransacaoDominio : ITransacaoDominio
    {
        private readonly IContaDominio _contaDominio;
        private readonly ITransacaoRepositorio _transacaoRepositorio;
        private readonly ITipoTransacaoRepositorio _tipoTransacaoRepositorio;
        public TransacaoDominio(IContaDominio contaDominio, ITransacaoRepositorio transacaoRepositorio, ITipoTransacaoRepositorio tipoTransacaoRepositorio)
        {
            _contaDominio = contaDominio;
            _transacaoRepositorio = transacaoRepositorio;
            _tipoTransacaoRepositorio = tipoTransacaoRepositorio;
        }

        public IList<TipoTransacaoDTO> BuscarTiposTransacoesDisponiveis()
        {
            var transacoes = _tipoTransacaoRepositorio.BuscarTiposTransacoes();
            transacoes = transacoes.Where(x => x.Nome != TiposTransacoes.RentabilizarCC).ToList();

            return transacoes.Select(x => new TipoTransacaoDTO 
            { 
                IdTipoTransacao = x.IdTipoTransacao,
                Nome = x.Nome,
                Descricao = x.Descricao,
                DescricaoAbreviada = x.DescricaoAbreviada,
                FlagCredito = x.FlagCredito,
                FlagSaldoAtual = x.FlagSaldoAtual
            }).ToList();
        }

        public TipoTransacao BuscarTipoTransacoesPorNome(string nome) =>
             _tipoTransacaoRepositorio.BuscarTiposTransacoesPorNome(nome);

        public void Inserir(TransacaoDTO transacaoParam)
        {
            var tipoTransacao = _tipoTransacaoRepositorio.BuscarTiposTransacoesPorId(transacaoParam.IdTipoTransacao);
            ValidarTipoTransacao(tipoTransacao);

            var conta = _contaDominio.BuscarPorId(transacaoParam.IdConta);
            _contaDominio.ValidarConta(conta);

            ValidarSaldo(conta, transacaoParam, tipoTransacao);

            conta = AdicionarTransacao(conta, transacaoParam, tipoTransacao);

            _transacaoRepositorio.InserirTransacao(conta);
        }

        public void ValidarTipoTransacao(TipoTransacao tipoTransacao)
        {
            if(tipoTransacao == null)
                throw new ArgumentException(MensagemResposta.TipoTransacaoInvalido);
        }

        public void ValidarSaldo(Conta conta, TransacaoDTO transacaoParam, TipoTransacao tipoTransacao)
        {
            decimal valorSaldoAtual = transacaoParam.Valor * tipoTransacao.FlagSaldoAtual;

            if(transacaoParam.Valor <= 0)
                throw new ArgumentException(MensagemResposta.ValorTransacaoInvalido);

            if(conta.SaldoAtual + valorSaldoAtual < 0)
                throw new ArgumentException(MensagemResposta.ContaSemSaldoParaOperacao);
        }

        public Conta AdicionarTransacao(Conta conta, TransacaoDTO transacaoParam, TipoTransacao tipoTransacao)
        {
            decimal valorSaldoAtual = transacaoParam.Valor * tipoTransacao.FlagSaldoAtual;

            conta.SaldoAtual = conta.SaldoAtual + valorSaldoAtual;

            conta.Transacao.Add(new Transacao 
            { 
                IdConta = transacaoParam.IdConta,
                DataHora = transacaoParam.DataHora,
                IdTipoTransacao = transacaoParam.IdTipoTransacao,
                Valor = transacaoParam.Valor,
                Historico = transacaoParam.Historico,
            });

            return conta;
        } 

    }
}
