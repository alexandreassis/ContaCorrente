using ContaCorrente.Commum;
using ContaCorrente.Dominio.DTO;
using ContaCorrente.Dominio.Interfaces;
using ContaCorrente.Repositorio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContaCorrente.Dominio.Dominios
{
    public class PessoaDominio : IPessoaDominio
    {
        private readonly IPessoaRepositorio _pessoaRepositorio;
        public PessoaDominio(IPessoaRepositorio pessoaRepositorio)
        {
            _pessoaRepositorio = pessoaRepositorio;
        }

        /// <summary>
        /// Buscar clientes paginado
        /// </summary>
        /// <param name="qtd"></param>
        /// <returns></returns>
        public IList<PessoaDTO> Buscar(int qtd = 10)
        {
            var pessoas = _pessoaRepositorio.Buscar(qtd);

            return pessoas.Select(item => new PessoaDTO
            {
                CPF = item.Cpf,
                IdPessoa = item.IdPessoa,
                Nome = item.Nome
            }).ToList();
        }

        /// <summary>
        /// Buscar cliente por idPessoa
        /// </summary>
        /// <param name="idPessoa"></param>
        /// <returns></returns>
        public PessoaDTO BuscarPorId(int idPessoa)
        {
            var pessoa = _pessoaRepositorio.BuscarPorId(idPessoa);

            if (pessoa == null)
                throw new ArgumentException(MensagemResposta.PessoaFisicaNaoEncontrada);

            return new PessoaDTO()
            {
                IdPessoa = pessoa.IdPessoa,
                CPF = pessoa.Cpf,
                Nome = pessoa.Nome
            };
        }

        /// <summary>
        /// Buscar cliente por CPF
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public PessoaDTO BuscarPorCPF(string cpf)
        {
            var pessoa = _pessoaRepositorio.BuscarPorCPF(cpf);

            if (pessoa == null)
                throw new ArgumentException(MensagemResposta.PessoaFisicaNaoEncontrada);

            return new PessoaDTO()
            {
                CPF = pessoa.Cpf,
                IdPessoa = pessoa.IdPessoa,
                Nome = pessoa.Nome
            }; ;
        }

        /// <summary>
        /// Inserir uma pessoa nova
        /// </summary>
        /// <param name="pessoa"></param>
        public void Inserir(PessoaDTO pessoa)
        {
            var p = _pessoaRepositorio.BuscarPorCPF(pessoa.CPF);
            if (p != null)
                throw new ArgumentException(MensagemResposta.PessoaFisicaJaExiste);

            _pessoaRepositorio.Inserir(pessoa.CPF, pessoa.Nome);
        }

        /// <summary>
        /// Atualizar dados de uma Pessoa
        /// </summary>
        /// <param name="pessoa"></param>
        public void Atualizar(PessoaDTO pessoa)
        {
            var p = _pessoaRepositorio.BuscarPorId(pessoa.IdPessoa);
            if (p == null)
                throw new ArgumentException(MensagemResposta.PessoaFisicaNaoEncontrada);

            _pessoaRepositorio.Atualizar(pessoa.IdPessoa, pessoa.Nome);
        }

    }
}
