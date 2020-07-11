using System;
using System.Collections.Generic;
using System.Text;

namespace ContaCorrente.Dominio.DTO
{
    public class TipoTransacaoDTO
    {
        public int IdTipoTransacao { get; set; }
        public string Nome { get; set; }
        public string DescricaoAbreviada { get; set; }
        public string Descricao { get; set; }
        public bool FlagCredito { get; set; }
        public short FlagSaldoAtual { get; set; }
    }
}
