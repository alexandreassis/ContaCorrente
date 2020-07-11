using ContaCorrente.Repositorio.Entities;
using ContaCorrente.Repositorio.Interfaces;
using ContaCorrente.Repositorio.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContaCorrente.Repositorio.Repositorios
{
    public class TransacaoRepositorio : ITransacaoRepositorio
    {
        private readonly CCDbContext _context;
        public TransacaoRepositorio(CCDbContext context) => _context = context;
        
        public IList<Transacao> BuscarTransacoesUltimosDias(int idConta, int dias = 30) =>
            _context.Transacao
                .Include(x => x.IdTipoTransacaoNavigation)
                .Where(x => x.IdConta == idConta)
                .Where(x=> x.DataHora >= DateTime.Today.AddDays(dias * -1))
                .AsNoTracking()
                .ToList();

        public void InserirTransacao(Conta conta)
        {
            _context.Update(conta);
            _context.SaveChanges();
        }

    }
}
