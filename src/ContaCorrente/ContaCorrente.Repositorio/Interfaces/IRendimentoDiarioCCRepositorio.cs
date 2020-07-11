using ContaCorrente.Repositorio.Entities;
using System;

namespace ContaCorrente.Repositorio.Interfaces
{
    public interface IRendimentoDiarioCCRepositorio
    {
        public TaxaCdi BuscarTaxa(DateTime data);
        public RendimentoDiarioCc BuscarUltimoRendimentoConta(int idConta);
    }
}
