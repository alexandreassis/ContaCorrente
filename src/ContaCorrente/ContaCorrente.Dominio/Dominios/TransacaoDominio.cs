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

        /// <summary>
        /// Busca os TipoTransacao disponiveis para serem lancada na conta manualmente.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Buca o TipoTransacao pelo nome dela.
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        public TipoTransacao BuscarTipoTransacoesPorNome(string nome) =>
             _tipoTransacaoRepositorio.BuscarTiposTransacoesPorNome(nome);

        /// <summary>
        /// Insere uma transacao nova na conta.
        /// </summary>
        /// <param name="transacaoParam"></param>
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

        /// <summary>
        /// Realiza a validacao basica do TipoTransacao a ser lancado
        /// </summary>
        /// <param name="tipoTransacao"></param>
        public void ValidarTipoTransacao(TipoTransacao tipoTransacao)
        {
            if(tipoTransacao == null)
                throw new ArgumentException(MensagemResposta.TipoTransacaoInvalido);
        }

        /// <summary>
        /// Realiza a validacao basica do Saldo da conta para o lancamento da transacao
        /// </summary>
        /// <param name="conta"></param>
        /// <param name="transacaoParam"></param>
        /// <param name="tipoTransacao"></param>
        public void ValidarSaldo(Conta conta, TransacaoDTO transacaoParam, TipoTransacao tipoTransacao)
        {
            decimal valorSaldoAtual = transacaoParam.Valor * tipoTransacao.FlagSaldoAtual;

            if(transacaoParam.Valor <= 0)
                throw new ArgumentException(MensagemResposta.ValorTransacaoInvalido);

            if(conta.SaldoAtual + valorSaldoAtual < 0)
                throw new ArgumentException(MensagemResposta.ContaSemSaldoParaOperacao);
        }

        /// <summary>
        /// Realiza os procedimentos necessarios para inclusao de uma nova transacao na conta.
        /// </summary>
        /// <param name="conta"></param>
        /// <param name="transacaoParam"></param>
        /// <param name="tipoTransacao"></param>
        /// <returns></returns>
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
