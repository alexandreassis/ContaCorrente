using ContaCorrente.Dominio.DTO;
using System.Collections.Generic;

namespace ContaCorrente.Dominio.Interfaces
{
    public interface IPessoaDominio
    {
        public IList<PessoaDTO> Buscar(int qtd);
        public PessoaDTO BuscarPorId(int idPessoa);
        public PessoaDTO BuscarPorCPF(string cpf);
        public PessoaDTO Inserir(PessoaDTO pessoa);
        public PessoaDTO Atualizar(PessoaDTO pessoa);
    }
}
