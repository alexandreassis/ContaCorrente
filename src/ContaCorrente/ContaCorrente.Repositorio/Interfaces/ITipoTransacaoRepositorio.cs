using ContaCorrente.Repositorio.Entities;
using System.Collections.Generic;

namespace ContaCorrente.Repositorio.Interfaces
{
    public interface ITipoTransacaoRepositorio
    {
        public IList<TipoTransacao> BuscarTiposTransacoes();
        public TipoTransacao BuscarTiposTransacoesPorId(int idTipoTransacao);
        public TipoTransacao BuscarTiposTransacoesPorNome(string descriao);

    }
}
