# Conta Corrente
Sistema de controle de conta corrente bancária, processando solicitações de depósito, resgates, pagamentos e rentabilizar diáriamente o dinheiro parado em conta.

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

## Instalação

- Todo o código é necessário para executar.
- Possuir ambiente Visual Studio 2019 instalado.
- Possuir um servidor SQL Server.

### Clone

- Clonar esse repositório na sua máquina usando o caminho <a href="https://github.com/alexandreassis/ContaCorrente" target="_blank">https://github.com/alexandreassis/ContaCorrente</a>.

### Setup

- Executar os scripts de criação da base disponíveis em <a href="https://github.com/alexandreassis/ContaCorrente/blob/master/docs/CreateTables.sql" target="_blank">CreateTables.sql</a>.

- Ajustar o server na connection string no arquivo `appsettings.json`:
```shell
"ConnectionStrings": {
    "CCDatabase": "server=DESKTOP-KR8LN7B\\SQLEXPRESS;Database=ContaCorrente;User Id=dev;Password=dev;"
}
```

- Compilar e rodar.
---

## Funcionalidades
- Criação e edição de Pessoas e Contas
- Operações de depósitos, resgates e pagamentos em uma conta.
- Sensibilizar saldo da conta de acordo com as operações realizadas.
- Realizar a rentabildiade do saldo parado em conta conforme taxa CDI diária.
Para fins de simplicação e testes, o sistema não esta verificando se é dia útil ou não.
Toda a primeira rentabilidade de uma conta usará como data de refenrência a data da operação menos 5 dias. Dessa maniera conseguimos simular 5 dias de rentabilidade.

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

