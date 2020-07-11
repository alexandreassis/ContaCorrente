using ContaCorrente.Repositorio.Entities;
using System.Collections.Generic;

namespace ContaCorrente.Repositorio.Interfaces
{
    public interface IPessoaRepositorio
    {
        public IList<Pessoa> Buscar(int qtd);
        public Pessoa BuscarPorId(int idPessoa);
        public Pessoa BuscarPorCPF(string cpf);
        public void Inserir(string cpf, string nome);
        public void Atualizar(int idPessoa, string nome);
    }
}
