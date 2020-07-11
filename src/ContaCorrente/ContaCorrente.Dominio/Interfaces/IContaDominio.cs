using ContaCorrente.Dominio.DTO;
using ContaCorrente.Repositorio.Entities;
using System.Collections.Generic;

namespace ContaCorrente.Dominio.Interfaces
{
    public interface IContaDominio
    {
        public IList<ContaDTO> Buscar(int qtd);
        public ContaDTO BuscarPorIdDetalhado(int idConta);
        public Conta BuscarPorId(int idConta);
        public void ValidarConta(Conta conta);
    }
}
