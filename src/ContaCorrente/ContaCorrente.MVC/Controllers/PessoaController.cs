using ContaCorrente.Commum;
using ContaCorrente.Dominio.DTO;
using ContaCorrente.Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ContaCorrente.MVC.Controllers
{
    public class PessoaController : Controller
    {
        private readonly IPessoaDominio _pessoaDominio;
        public PessoaController(IPessoaDominio pessoaDominio)
        {
            _pessoaDominio = pessoaDominio;
        }

        [HttpGet]
        [Route("Pessoa")]
        public IActionResult Index()
        {
            return View(_pessoaDominio.Buscar(10));
        }

        #region Criar

        [HttpGet]
        [Route("Pessoa/Criar")]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [Route("Pessoa/Criar")]
        public IActionResult Criar([Bind("Nome,CPF")] PessoaDTO pessoa)
        {
            if (!ModelState.IsValid)
                return View(pessoa);

            try
            {
                _pessoaDominio.Inserir(pessoa);

                return Ok(MensagemResposta.PessoaInseridaSucesso);
            }
            catch (ArgumentException argumentEx)
            {
                return BadRequest(argumentEx.Message);
            }
        }

        #endregion


        #region Editar

        [HttpGet]
        [Route("Pessoa/Editar")]
        public IActionResult Editar(int? id)
        {
            if (id == null)
                return NotFound();

            return View(_pessoaDominio.BuscarPorId(id.Value));
        }

        [HttpPost]
        [Route("Pessoa/Editar")]
        public IActionResult Editar([Bind("IdPessoa,Nome,CPF")] PessoaDTO pessoa)
        {
            if (!ModelState.IsValid)
                return View(pessoa);
            
            try
            {
                _pessoaDominio.Atualizar(pessoa);

                return Ok(MensagemResposta.PessoaAtualizadaSucesso);
            }
            catch (ArgumentException argumentEx)
            {
                return BadRequest(argumentEx.Message);
            }
        }

        #endregion

    }
}