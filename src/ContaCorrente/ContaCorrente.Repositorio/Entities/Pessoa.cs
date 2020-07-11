using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContaCorrente.Repositorio.Entities
{
    public partial class Pessoa
    {
        public Pessoa()
        {
            Conta = new HashSet<Conta>();
        }

        [Key]
        [Column("Id_Pessoa")]
        public int IdPessoa { get; set; }
        [Required]
        [Column("CPF")]
        [StringLength(11)]
        public string Cpf { get; set; }
        [Required]
        [StringLength(60)]
        public string Nome { get; set; }

        [InverseProperty("IdPessoaNavigation")]
        public virtual ICollection<Conta> Conta { get; set; }
    }
}
