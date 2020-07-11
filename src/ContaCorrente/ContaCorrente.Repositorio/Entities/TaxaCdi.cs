using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContaCorrente.Repositorio.Entities
{
    [Table("TaxaCDI")]
    public partial class TaxaCdi
    {
        public TaxaCdi()
        {
            RendimentoDiarioCc = new HashSet<RendimentoDiarioCc>();
        }

        [Key]
        [Column("Id_TaxaCDI")]
        public int IdTaxaCdi { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Data { get; set; }
        [Column(TypeName = "numeric(10, 8)")]
        public decimal TaxaDia { get; set; }
        [Column(TypeName = "numeric(10, 8)")]
        public decimal Percentual { get; set; }

        [InverseProperty("IdTaxaCdiNavigation")]
        public virtual ICollection<RendimentoDiarioCc> RendimentoDiarioCc { get; set; }
    }
}
