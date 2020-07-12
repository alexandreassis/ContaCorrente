using ContaCorrente.Dominio.DTO;
using ContaCorrente.Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContaCorrente.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaDominio _pessoaDominio;
        public PessoaController(IPessoaDominio pessoaDominio)
        {
            _pessoaDominio = pessoaDominio;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PessoaDTO>> GetPessoa()
        {
            return _pessoaDominio.Buscar(10).ToArray();
        }

        [HttpGet("{id}")]
        public ActionResult<PessoaDTO> GetPessoa(int id)
        {
            //var pessoa = await _pessoaDominio.BuscarPorId(id);
            var pessoa = _pessoaDominio.BuscarPorId(id);
            
            if (pessoa == null)
                return NotFound();

            return pessoa;
        }

        [HttpPost]
        public ActionResult<PessoaDTO> PostPessoa(PessoaDTO pessoaParam)
        {
            try
            {
                var pessoa = _pessoaDominio.Inserir(pessoaParam);

                return CreatedAtAction("GetPessoa", new { id = pessoa.IdPessoa }, pessoaParam);
            }
            catch (ArgumentException argumentEx)
            {
                return BadRequest(argumentEx.Message);
            }
        }

        [HttpPut]
        public ActionResult<PessoaDTO> PutPessoa(PessoaDTO pessoaParam)
        {
            try
            {
                var pessoa = _pessoaDominio.Atualizar(pessoaParam);

                return CreatedAtAction("GetPessoa", new { id = pessoa.IdPessoa }, pessoaParam);
            }
            catch (ArgumentException argumentEx)
            {
                return BadRequest(argumentEx.Message);
            }
        }

    }
}