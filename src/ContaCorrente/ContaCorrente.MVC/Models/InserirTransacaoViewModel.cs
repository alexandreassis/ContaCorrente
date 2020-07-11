using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContaCorrente.MVC.Models
{
    public class InserirTransacaoViewModel
    {
        public int IdConta { get; set; }
        public int IdTipoTransacao { get; set; }
        public decimal Valor { get; set; }
        public SelectList TiposTransacoes { get; set; }
    }
}
