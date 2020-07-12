using ContaCorrente.Commum;
using ContaCorrente.Dominio.DTO;
using ContaCorrente.Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContaCorrente.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransacaoController : ControllerBase
    {
        private readonly ITransacaoDominio _transacaoDominio;
        private readonly IRendimentoDiarioDominio _rendimentoDiarioDominio;
        public TransacaoController(ITransacaoDominio transacaoDominio, IRendimentoDiarioDominio rendimentoDiarioDominio)
        {
            _transacaoDominio = transacaoDominio;
            _rendimentoDiarioDominio = rendimentoDiarioDominio;
        }

        [HttpGet]
        [Route("/transacao/BuscarTipoTransacao")]
        public ActionResult<IEnumerable<TipoTransacaoDTO>> BuscarTipoTransacao()
        {
            return _transacaoDominio.BuscarTiposTransacoesDisponiveis().ToArray();
        }

        [HttpPost]
        public ActionResult<TransacaoDTO> Post(TransacaoDTO transacao)
        {
            try
            {
                transacao.DataHora = DateTime.Now;

                _transacaoDominio.Inserir(transacao);

                return transacao;
            }
            catch (ArgumentException argumentEx)
            {
                return BadRequest(argumentEx.Message);
            }
        }

        [HttpPost]
        [Route("/transacao/LancarRendimentoDiarioCC")]
        public ActionResult<bool> LancarRendimentoDiarioCC(int? idConta)
        {
            if (idConta == null)
                return NotFound();

            try
            {
                _rendimentoDiarioDominio.LancarRendimentoDiarioCC(idConta.Value);

                return true;
            }
            catch (ArgumentException argumentEx)
            {
                return BadRequest(argumentEx.Message);
            }
        }
    }
}