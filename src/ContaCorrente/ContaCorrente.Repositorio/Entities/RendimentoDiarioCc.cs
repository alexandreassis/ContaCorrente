using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContaCorrente.Repositorio.Entities
{
    [Table("RendimentoDiarioCC")]
    public partial class RendimentoDiarioCc
    {
        [Key]
        [Column("Id_RendimentoDiarioCC")]
        public int IdRendimentoDiarioCc { get; set; }
        [Column("Id_Conta")]
        public int IdConta { get; set; }
        [Column("Id_TaxaCDI")]
        public int IdTaxaCdi { get; set; }
        [Column(TypeName = "money")]
        public decimal SaldoAtual { get; set; }
        [Column(TypeName = "money")]
        public decimal Rendimento { get; set; }

        [ForeignKey(nameof(IdConta))]
        [InverseProperty(nameof(Conta.RendimentoDiarioCc))]
        public virtual Conta IdContaNavigation { get; set; }
        [ForeignKey(nameof(IdTaxaCdi))]
        [InverseProperty(nameof(TaxaCdi.RendimentoDiarioCc))]
        public virtual TaxaCdi IdTaxaCdiNavigation { get; set; }
    }
}
