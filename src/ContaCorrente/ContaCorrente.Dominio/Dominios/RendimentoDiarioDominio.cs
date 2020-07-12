using ContaCorrente.Commum;
using ContaCorrente.Dominio.DTO;
using ContaCorrente.Dominio.Interfaces;
using ContaCorrente.Repositorio.Entities;
using ContaCorrente.Repositorio.Interfaces;
using System;

namespace ContaCorrente.Dominio.Dominios
{
    public class RendimentoDiarioDominio : IRendimentoDiarioDominio
    {
        private readonly IContaDominio _contaDominio;
        private readonly ITransacaoDominio _transacaoDominio;
        private readonly IRendimentoDiarioCCRepositorio _rendimentoDiarioCCRepositorio;
        private readonly ITransacaoRepositorio _transacaoRepositorio;
        public RendimentoDiarioDominio(IContaDominio contaDominio, ITransacaoDominio transacaoDominio, IRendimentoDiarioCCRepositorio rendimentoDiarioCCRepositorio, ITransacaoRepositorio transacaoRepositorio)
        {
            _contaDominio = contaDominio;
            _transacaoDominio = transacaoDominio;
            _rendimentoDiarioCCRepositorio = rendimentoDiarioCCRepositorio;
            _transacaoRepositorio = transacaoRepositorio;
        }

        /// <summary>
        /// Realiza o lancamento do rendimento diario da conta corrente para a conta
        /// </summary>
        /// <param name="idConta"></param>
        public void LancarRendimentoDiarioCC(int idConta)
        {
            //Buscar dados a conta
            var conta = _contaDominio.BuscarPorId(idConta);
            _contaDominio.ValidarConta(conta);

            if(conta.SaldoAtual <= 0)
                throw new ArgumentException(MensagemResposta.ContaSemSaldoParaOperacao);

            //Buscar dados da transacao
            var tipoTransacao = _transacaoDominio.BuscarTipoTransacoesPorNome(TiposTransacoes.RentabilizarCC);
            _transacaoDominio.ValidarTipoTransacao(tipoTransacao);

            //Buscar proximo rendimento
            var proximoRendimento = BuscarProximoRendimento(idConta);

            //Relizar o calculo com os dados retornados. 
            var transacao = CalcularRendimento(conta.IdConta, conta.SaldoAtual, tipoTransacao.IdTipoTransacao, proximoRendimento);

            //Insere a nova transacao na conta.
            conta = _transacaoDominio.AdicionarTransacao(conta, transacao, tipoTransacao);
            conta.RendimentoDiarioCc.Add(new RendimentoDiarioCc
            {
                IdConta = transacao.IdConta,
                IdTaxaCdi = proximoRendimento.IdTaxaCdi,
                Rendimento = transacao.Valor,
                SaldoAtual = conta.SaldoAtual
            });

            _transacaoRepositorio.InserirTransacao(conta);
        }

        /// <summary>
        /// Busca as informacoes do proximo rendimento a ser processado na conta.
        /// </summary>
        /// <param name="idConta"></param>
        /// <returns></returns>
        public TaxaCdi BuscarProximoRendimento(int idConta)
        {
            //Buscar o ultimo rendimento processado
            var ultimoRendimento = _rendimentoDiarioCCRepositorio.BuscarUltimoRendimentoConta(idConta);

            //Buscar proximo dia a processar. 
            //Se cliente não tem rendimentos ainda considerar DateTime.Now.AddDays(-5), caso contrario usar o do ultimo dia + 1.
            DateTime dataProximoRendimento = ultimoRendimento == null ? DateTime.Today.AddDays(-5) : ultimoRendimento.IdTaxaCdiNavigation.Data.AddDays(1);

            if (dataProximoRendimento >= DateTime.Today)
                throw new ArgumentException(MensagemResposta.DataRendimentoCCInvalido);

            return _rendimentoDiarioCCRepositorio.BuscarTaxa(dataProximoRendimento);
        }

        /// <summary>
        /// Realiza o calculo de rendimento e retorna a transacao que devera ser incluida.
        /// </summary>
        /// <param name="idConta"></param>
        /// <param name="saldoAtual"></param>
        /// <param name="idTipoTransacao"></param>
        /// <param name="proximoRendimento"></param>
        /// <returns></returns>
        public TransacaoDTO CalcularRendimento(int idConta, decimal saldoAtual, int idTipoTransacao, TaxaCdi proximoRendimento)
        {
            var transacao = new TransacaoDTO()
            {
                IdConta = idConta,
                IdTipoTransacao = idTipoTransacao,
                Historico = proximoRendimento.Data.ToString("dd/MM/yyyy"),
                DataHora = DateTime.Now,
            };

            decimal rendimento = saldoAtual * (proximoRendimento.Percentual * proximoRendimento.TaxaDia);

            transacao.Valor = rendimento;

            return transacao;
        }

    }
}
