using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContaCorrente.Repositorio.Entities
{
    public partial class Conta
    {
        public Conta()
        {
            RendimentoDiarioCc = new HashSet<RendimentoDiarioCc>();
            Transacao = new HashSet<Transacao>();
        }

        [Key]
        [Column("Id_Conta")]
        public int IdConta { get; set; }
        [Column("Id_Pessoa")]
        public int IdPessoa { get; set; }
        [Column(TypeName = "money")]
        public decimal SaldoAtual { get; set; }

        [ForeignKey(nameof(IdPessoa))]
        [InverseProperty(nameof(Pessoa.Conta))]
        public virtual Pessoa IdPessoaNavigation { get; set; }
        [InverseProperty("IdContaNavigation")]
        public virtual ICollection<RendimentoDiarioCc> RendimentoDiarioCc { get; set; }
        [InverseProperty("IdContaNavigation")]
        public virtual ICollection<Transacao> Transacao { get; set; }
    }
}
