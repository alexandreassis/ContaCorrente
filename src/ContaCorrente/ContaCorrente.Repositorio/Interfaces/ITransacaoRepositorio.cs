using ContaCorrente.Repositorio.Entities;
using System.Collections.Generic;

namespace ContaCorrente.Repositorio.Interfaces
{
    public interface ITransacaoRepositorio
    {
        public IList<Transacao> BuscarTransacoesUltimosDias(int idConta, int dias);
        public void InserirTransacao(Conta conta);
    }
}
