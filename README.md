# Conta Corrente
Sistema de controle de conta corrente banc�ria, processando solicita��es de dep�sito, resgates, pagamentos e rentabilizar di�riamente o dinheiro parado em conta.

[![Cadastro de Pessoas](https://github.com/alexandreassis/ContaCorrente/blob/master/docs/Images/02_Pessoas_03.png?raw=true "Cadastro de Pessoas")]()

[![Cadastro de Contas](https://github.com/alexandreassis/ContaCorrente/blob/master/docs/Images/02_Contas_08.png?raw=true "Cadastro de Contas")]()

## Tecnologias e arquitetura
- C#
- ASP.Net Core 
- Angular 8
- Entity Framework
- SqlServer
- DDD

---

## Instala��o

- Todo o c�digo � necess�rio para executar.
- Possuir ambiente Visual Studio 2019 instalado.
- Possuir um servidor SQL Server.

### Clone

- Clonar esse reposit�rio na sua m�quina usando o caminho <a href="https://github.com/alexandreassis/ContaCorrente" target="_blank">https://github.com/alexandreassis/ContaCorrente</a>.

### Setup

- Executar os scripts de cria��o da base dispon�veis em <a href="https://github.com/alexandreassis/ContaCorrente/blob/master/docs/CreateTables.sql" target="_blank">CreateTables.sql</a>.

- Ajustar o server na connection string no arquivo `appsettings.json`:
```shell
"ConnectionStrings": {
    "CCDatabase": "server=DESKTOP-KR8LN7B\\SQLEXPRESS;Database=ContaCorrente;User Id=dev;Password=dev;"
}
```

- Compilar e rodar.
---

## Funcionalidades
- Cria��o e edi��o de Pessoas e Contas
- Opera��es de dep�sitos, resgates e pagamentos em uma conta.
- Sensibilizar saldo da conta de acordo com as opera��es realizadas.
- Realizar a rentabildiade do saldo parado em conta conforme taxa CDI di�ria.
Para fins de simplica��o e testes, o sistema n�o esta verificando se � dia �til ou n�o.
Toda a primeira rentabilidade de uma conta usar� como data de refenr�ncia a data da opera��o menos 5 dias. Dessa maniera conseguimos simular 5 dias de rentabilidade.

## Exemplos
[![](https://github.com/alexandreassis/ContaCorrente/blob/master/docs/Images/01_Home.png?raw=true "")]()
[![](https://github.com/alexandreassis/ContaCorrente/blob/master/docs/Images/02_Pessoas_01.png?raw=true "")]()
[![](https://github.com/alexandreassis/ContaCorrente/blob/master/docs/Images/02_Pessoas_02.png?raw=true "")]()
[![](https://github.com/alexandreassis/ContaCorrente/blob/master/docs/Images/02_Pessoas_03.png?raw=true "")]()
[![](https://github.com/alexandreassis/ContaCorrente/blob/master/docs/Images/02_Pessoas_04.png?raw=true "")]()
[![](https://github.com/alexandreassis/ContaCorrente/blob/master/docs/Images/02_Contas_01.png?raw=true "")]()
[![](https://github.com/alexandreassis/ContaCorrente/blob/master/docs/Images/02_Contas_02.png?raw=true "")]()
[![](https://github.com/alexandreassis/ContaCorrente/blob/master/docs/Images/02_Contas_03.png?raw=true "")]()
[![](https://github.com/alexandreassis/ContaCorrente/blob/master/docs/Images/02_Contas_04.png?raw=true "")]()
[![](https://github.com/alexandreassis/ContaCorrente/blob/master/docs/Images/02_Contas_05.png?raw=true "")]()
[![](https://github.com/alexandreassis/ContaCorrente/blob/master/docs/Images/02_Contas_06.png?raw=true "")]()
[![](https://github.com/alexandreassis/ContaCorrente/blob/master/docs/Images/02_Contas_07.png?raw=true "")]()
[![](https://github.com/alexandreassis/ContaCorrente/blob/master/docs/Images/02_Contas_08.png?raw=true "")]()

