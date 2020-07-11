using System.Collections.Generic;

namespace ContaCorrente.Dominio.DTO
{
    public class ContaDTO
    {
        public int IdConta { get; set; }
        public int IdPessoa { get; set; }
        public decimal SaldoAtual { get; set; }
        public PessoaDTO Pessoa { get; set; }
        public IList<TransacaoDTO> Transacoes { get; set; }
    }
}
