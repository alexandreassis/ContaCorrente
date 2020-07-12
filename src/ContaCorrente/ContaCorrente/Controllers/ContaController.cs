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
    public class ContaController : ControllerBase
    {
        private readonly IContaDominio _contaDominio;
        public ContaController(IContaDominio contaDominio)
        {
            _contaDominio = contaDominio;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ContaDTO>> GetContas()
        {
            return _contaDominio.Buscar(10).ToArray();
        }

        [HttpGet]
        [Route("/conta/InfosAdicionais")]
        public ActionResult<ContaDTO> InfosAdicionais(int? id)
        {
            if (id == null || id.Value <= 0)
                return NotFound();

            return _contaDominio.BuscarPorIdDetalhado(id.Value);
        }

    }
}