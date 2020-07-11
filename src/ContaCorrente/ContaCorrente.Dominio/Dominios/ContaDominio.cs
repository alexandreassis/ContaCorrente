﻿using ContaCorrente.Commum;
using ContaCorrente.Dominio.DTO;
using ContaCorrente.Dominio.Interfaces;
using ContaCorrente.Dominio.Mapper;
using ContaCorrente.Repositorio.Entities;
using ContaCorrente.Repositorio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContaCorrente.Dominio.Dominios
{
    public class ContaDominio : IContaDominio
    {
        private readonly IContaRepositorio _contaRepositorio;
        private readonly IPessoaRepositorio _pessoaRepositorio;
        private readonly ITransacaoRepositorio _transacaoRepositorio;
        public ContaDominio(IContaRepositorio contaRepositorio, IPessoaRepositorio pessoaRepositorio, ITransacaoRepositorio transacaoRepositorio)
        {
            _contaRepositorio = contaRepositorio;
            _pessoaRepositorio = pessoaRepositorio;
            _transacaoRepositorio = transacaoRepositorio;
        }

        /// <summary>
        /// Buscar contas paginado
        /// </summary>
        /// <param name="qtd"></param>
        /// <returns></returns>
        public IList<ContaDTO> Buscar(int qtd = 10)
        {
            var contas = _contaRepositorio.Buscar(qtd);

            return contas.Select(item => new ContaDTO
            {
                IdConta = item.IdConta,
                IdPessoa = item.IdPessoa,
                SaldoAtual = item.SaldoAtual,
            }).ToList();
        }

        public Conta BuscarPorId(int idConta)
        {
            return _contaRepositorio.BuscarPorId(idConta);
        }

        /// <summary>
        /// Busca informações da conta com transações detalhadas
        /// </summary>
        /// <param name="idConta"></param>
        /// <returns></returns>
        public ContaDTO BuscarPorIdDetalhado(int idConta)
        {
            var conta = _contaRepositorio.BuscarPorId(idConta);
            var pessoa = _pessoaRepositorio.BuscarPorId(conta.IdPessoa);
            var transacoes = _transacaoRepositorio.BuscarTransacoesUltimosDias(idConta, 30);

            return conta.ConvertToDTO(pessoa, transacoes); ;
        }

        public void ValidarConta(Conta conta)
        {
            if (conta == null)
                throw new ArgumentException(MensagemResposta.ContaInvalida);
        }

    }
}
