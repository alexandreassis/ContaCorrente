using ContaCorrente.Repositorio.Entities;
using ContaCorrente.Repositorio.Interfaces;
using ContaCorrente.Repositorio.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContaCorrente.Repositorio.Repositorios
{
    public class PessoaRepositorio : IPessoaRepositorio
    {
        private readonly CCDbContext _context;
        public PessoaRepositorio(CCDbContext context) => _context = context;

        public IList<Pessoa> Buscar(int qtd) =>
            _context.Pessoa.OrderBy(x => x.IdPessoa).Take(qtd).ToList();

        public Pessoa BuscarPorId(int idPessoa) =>
            _context.Pessoa.Where(x => x.IdPessoa == idPessoa).FirstOrDefault();

        public Pessoa BuscarPorCPF(string cpf) =>
            _context.Pessoa.Where(x => x.Cpf == cpf).FirstOrDefault();

        public Pessoa Inserir(string cpf, string nome)
        {
            try
            {
                var pessoa = new Pessoa()
                {
                    Cpf = cpf,
                    Nome = nome,
                    Conta = new List<Conta>() {
                        new Conta() { SaldoAtual = 0}
                    }
                };

                _context.Pessoa.Add(pessoa);
                _context.SaveChanges();

                return pessoa;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Pessoa Atualizar(int idPessoa, string nome)
        {
            try
            {
                var pessoa = BuscarPorId(idPessoa);
                pessoa.Nome = nome;

                _context.Update(pessoa);
                _context.SaveChanges();

                return pessoa;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
