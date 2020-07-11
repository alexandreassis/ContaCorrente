using ContaCorrente.Commum;
using ContaCorrente.Dominio.DTO;
using ContaCorrente.Dominio.Interfaces;
using ContaCorrente.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace ContaCorrente.MVC.Controllers
{
    public class ContaController : Controller
    {
        private readonly IContaDominio _contaDominio;
        private readonly ITransacaoDominio _transacaoDominio;
        public ContaController(IContaDominio contaDominio, ITransacaoDominio transacaoDominio)
        {
            _contaDominio = contaDominio;
            _transacaoDominio = transacaoDominio;
        }

        [HttpGet]
        [Route("Conta")]
        public IActionResult Index()
        {
            return View(_contaDominio.Buscar(10));
        }

        [HttpGet]
        [Route("Conta/InfosAdicionais")]
        public IActionResult InfosAdicionais(int? id)
        {
            if (id == null)
                return NotFound();

            return View(_contaDominio.BuscarPorIdDetalhado(id.Value));
        }

        [HttpGet]
        [Route("Conta/InserirTransacao")]
        public IActionResult InserirTransacao(int? id)
        {
            if (id == null)
                return NotFound();

            var tiposTransacoes = _transacaoDominio.BuscarTiposTransacoesDisponiveis();

            var tiposTransacoesResp = new SelectList(tiposTransacoes.Select(x =>
                new SelectListItem
                {
                    Text = x.DescricaoAbreviada,
                    Value = x.IdTipoTransacao.ToString()
                }).ToList(), "Value", "Text");

            return View(new InserirTransacaoViewModel { IdConta = id.Value, TiposTransacoes = tiposTransacoesResp });
        }

        [HttpPost]
        [Route("Conta/InserirTransacao")]
        public IActionResult InserirTransacao([Bind("IdConta,Valor,IdTipoTransacao")] InserirTransacaoViewModel transacaoParam)
        {
            if (!ModelState.IsValid)
                return View(transacaoParam);

            try
            {
                var transacao = new TransacaoDTO()
                {
                    IdConta = transacaoParam.IdConta,
                    IdTipoTransacao = transacaoParam.IdTipoTransacao,
                    Valor = transacaoParam.Valor,
                    DataHora = DateTime.Now,
                };

                _transacaoDominio.Inserir(transacao);

                return Ok(MensagemResposta.TransacaoInseridaSucesso);
            }
            catch (ArgumentException argumentEx)
            {
                return BadRequest(argumentEx.Message);
            }
        }

    }
}