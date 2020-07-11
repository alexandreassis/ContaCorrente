using ContaCorrente.Dominio.DTO;
using ContaCorrente.Repositorio.Entities;
using System.Collections.Generic;

namespace ContaCorrente.Dominio.Interfaces
{
    public interface ITransacaoDominio
    {
        public IList<TipoTransacaoDTO> BuscarTiposTransacoesDisponiveis();
        public TipoTransacao BuscarTipoTransacoesPorNome(string nome);
        public void Inserir(TransacaoDTO transacao);
        public void ValidarTipoTransacao(TipoTransacao tipoTransacao);
        public void ValidarSaldo(Conta conta, TransacaoDTO transacaoParam, TipoTransacao tipoTransacao);
        public Conta AdicionarTransacao(Conta conta, TransacaoDTO transacaoParam, TipoTransacao tipoTransacao);
    }
}
