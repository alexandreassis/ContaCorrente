namespace ContaCorrente.Commum
{
    public static class MensagemResposta
    {
        public static readonly string PessoaFisicaJaExiste = "Pessoa Física já existente.";
        public static readonly string PessoaFisicaNaoEncontrada = "Pessoa Física não encontrada.";
        public static readonly string PessoaInseridaSucesso = "Cliente e Conta inserido com sucesso.";
        public static readonly string PessoaAtualizadaSucesso = "Cliente atualizado com sucesso.";

        public static readonly string ContaInvalida = "Conta inválida.";
        public static readonly string TipoTransacaoInvalido = "Tipo Transacao inválida.";
        public static readonly string ValorTransacaoInvalido = "Valor de transação inválido.";
        public static readonly string ContaSemSaldoParaOperacao = "Conta sem saldo para a operação solicitada.";

        public static readonly string TransacaoInseridaSucesso = "Transacao inserida com sucesso.";

        public static readonly string DataRendimentoCCInvalido = "Não há rendimentos diários a serem processados para a conta.";

    }
}
