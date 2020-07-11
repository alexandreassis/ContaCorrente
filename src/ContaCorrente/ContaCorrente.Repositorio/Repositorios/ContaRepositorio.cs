using ContaCorrente.Repositorio.Entities;
using ContaCorrente.Repositorio.Interfaces;
using ContaCorrente.Repositorio.Model;
using System.Collections.Generic;
using System.Linq;

namespace ContaCorrente.Repositorio.Repositorios
{
    public class ContaRepositorio : IContaRepositorio
    {
        private readonly CCDbContext _context;
        public ContaRepositorio(CCDbContext context) => _context = context;

        public IList<Conta> Buscar(int qtd) =>
            _context.Conta.OrderBy(x => x.IdConta).Take(qtd).ToList();

        public Conta BuscarPorId(int idConta) =>
            _context.Conta.Where(x=> x.IdConta == idConta).FirstOrDefault();

    }
}
