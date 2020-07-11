using ContaCorrente.Repositorio.Entities;
using System.Collections.Generic;

namespace ContaCorrente.Repositorio.Interfaces
{
    public interface IContaRepositorio
    {
        public IList<Conta> Buscar(int qtd);
        public Conta BuscarPorId(int idConta);
    }
}
