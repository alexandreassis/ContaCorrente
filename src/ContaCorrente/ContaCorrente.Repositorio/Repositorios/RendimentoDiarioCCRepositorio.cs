using ContaCorrente.Repositorio.Entities;
using ContaCorrente.Repositorio.Interfaces;
using ContaCorrente.Repositorio.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ContaCorrente.Repositorio.Repositorios
{
    public class RendimentoDiarioCCRepositorio: IRendimentoDiarioCCRepositorio
    {
        private readonly CCDbContext _context;
        public RendimentoDiarioCCRepositorio(CCDbContext context) => _context = context;

        public RendimentoDiarioCc BuscarUltimoRendimentoConta(int idConta) =>
            _context.RendimentoDiarioCc
                .Include(x=> x.IdTaxaCdiNavigation)
                .Where(x => x.IdConta >= idConta)
                .OrderByDescending(x => x.IdTaxaCdiNavigation.Data).FirstOrDefault();
        
        public TaxaCdi BuscarTaxa(DateTime data) =>
            _context.TaxaCdi.Where(x => x.Data >= data).OrderBy(x => x.Data).FirstOrDefault();
    }
}
