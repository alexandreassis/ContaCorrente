using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContaCorrente.Repositorio.Entities
{
    public partial class TipoTransacao
    {
        public TipoTransacao()
        {
            Transacao = new HashSet<Transacao>();
        }

        [Key]
        [Column("Id_TipoTransacao")]
        public int IdTipoTransacao { get; set; }
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }
        [Required]
        [StringLength(60)]
        public string DescricaoAbreviada { get; set; }
        [Required]
        [StringLength(80)]
        public string Descricao { get; set; }
        public bool FlagCredito { get; set; }
        public short FlagSaldoAtual { get; set; }

        [InverseProperty("IdTipoTransacaoNavigation")]
        public virtual ICollection<Transacao> Transacao { get; set; }
    }
}
