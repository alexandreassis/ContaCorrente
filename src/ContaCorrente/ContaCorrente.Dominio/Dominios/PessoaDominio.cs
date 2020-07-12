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
        /// Buscar as primeiras pessoas conforme parametro
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
        /// Buscar pessoa por idPessoa
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
        /// Inserir uma pessoa nova
        /// </summary>
        /// <param name="pessoa"></param>
        public PessoaDTO Inserir(PessoaDTO pessoaParam)
        {
            var p = _pessoaRepositorio.BuscarPorCPF(pessoaParam.CPF);
            if (p != null)
                throw new ArgumentException(MensagemResposta.PessoaFisicaJaExiste);

            var pessoa = _pessoaRepositorio.Inserir(pessoaParam.CPF, pessoaParam.Nome);

            return new PessoaDTO
            {
                IdPessoa = pessoa.IdPessoa,
                CPF = pessoa.Cpf,
                Nome = pessoa.Nome
            };
        }

        /// <summary>
        /// Atualizar dados de uma pessoa
        /// </summary>
        /// <param name="pessoa"></param>
        public PessoaDTO Atualizar(PessoaDTO pessoaParam)
        {
            var p = _pessoaRepositorio.BuscarPorId(pessoaParam.IdPessoa);
            if (p == null)
                throw new ArgumentException(MensagemResposta.PessoaFisicaNaoEncontrada);

            var pessoa = _pessoaRepositorio.Atualizar(pessoaParam.IdPessoa, pessoaParam.Nome);
            return new PessoaDTO
            {
                IdPessoa = pessoa.IdPessoa,
                CPF = pessoa.Cpf,
                Nome = pessoa.Nome
            };
        }

    }
}
