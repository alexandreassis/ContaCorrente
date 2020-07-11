using System;

namespace ContaCorrente.Dominio.DTO
{
    public class TransacaoDTO
    {
        public int IdTransacao { get; set; }
        public int IdTipoTransacao { get; set; }
        public int IdConta { get; set; }
        public DateTime DataHora { get; set; }
        public decimal Valor { get; set; }
        public string Historico { get; set; }
        public TipoTransacaoDTO TipoTransacao { get; set; }
    }
}
