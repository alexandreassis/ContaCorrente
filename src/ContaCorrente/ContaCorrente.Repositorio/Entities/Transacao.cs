using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContaCorrente.Repositorio.Entities
{
    public partial class Transacao
    {
        [Key]
        [Column("Id_Transacao")]
        public int IdTransacao { get; set; }
        [Column("Id_TipoTransacao")]
        public int IdTipoTransacao { get; set; }
        [Column("Id_Conta")]
        public int IdConta { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHora { get; set; }
        [Column(TypeName = "money")]
        public decimal Valor { get; set; }
        [StringLength(60)]
        public string Historico { get; set; }

        [ForeignKey(nameof(IdConta))]
        [InverseProperty(nameof(Conta.Transacao))]
        public virtual Conta IdContaNavigation { get; set; }
        [ForeignKey(nameof(IdTipoTransacao))]
        [InverseProperty(nameof(TipoTransacao.Transacao))]
        public virtual TipoTransacao IdTipoTransacaoNavigation { get; set; }
    }
}
