using ContaCorrente.Repositorio.Entities;
using ContaCorrente.Repositorio.Interfaces;
using ContaCorrente.Repositorio.Model;
using System.Collections.Generic;
using System.Linq;

namespace ContaCorrente.Repositorio.Repositorios
{
    public class TipoTransacaoRepositorio : ITipoTransacaoRepositorio
    {
        private readonly CCDbContext _context;
        public TipoTransacaoRepositorio(CCDbContext context) => _context = context;

        public IList<TipoTransacao> BuscarTiposTransacoes() => 
            _context.TipoTransacao.ToList();

        public TipoTransacao BuscarTiposTransacoesPorId(int idTipoTransacao) =>
            _context.TipoTransacao.Where(x => x.IdTipoTransacao == idTipoTransacao).FirstOrDefault();

        public TipoTransacao BuscarTiposTransacoesPorNome(string nome) => 
            _context.TipoTransacao.Where(x=> x.Nome == nome).FirstOrDefault();

    }
}
