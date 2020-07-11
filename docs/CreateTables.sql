SET NOCOUNT ON;  

USE master;

IF NOT EXISTS(SELECT TOP 1 1 FROM SYS.SERVER_PRINCIPALS WHERE NAME = 'dev')
BEGIN
	CREATE LOGIN dev WITH PASSWORD=N'dev', DEFAULT_DATABASE=master,CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF;

	ALTER SERVER ROLE sysadmin ADD MEMBER dev;
END
GO

IF(DB_ID(N'ContaCorrente') IS NULL)
	CREATE DATABASE ContaCorrente;
GO

USE ContaCorrente;

DROP TABLE IF EXISTS RendimentoDiarioCC
DROP TABLE IF EXISTS TaxaCDI
DROP TABLE IF EXISTS Transacao;
DROP TABLE IF EXISTS TipoTransacao;
DROP TABLE IF EXISTS Conta;
DROP TABLE IF EXISTS Pessoa;

CREATE TABLE Pessoa 
(
      Id_Pessoa INT IDENTITY NOT NULL PRIMARY KEY
    , CPF VARCHAR(11) NOT NULL
    , Nome VARCHAR(60) NOT NULL
);

--CREATE TABLE PessoaFisicaCadastro     --FOREIGN KEY Id_Pessoa
--CREATE TABLE PessoaJuridicaCadastro   --FOREIGN KEY Id_Pessoa

CREATE TABLE Conta 
(
      Id_Conta INT IDENTITY NOT NULL PRIMARY KEY
    , Id_Pessoa INT NOT NULL
    , SaldoAtual MONEY NOT NULL DEFAULT 0 --Deveria estar em uma tabela separada
    , FOREIGN KEY (Id_Pessoa) REFERENCES Pessoa(Id_Pessoa)
);

CREATE TABLE TipoTransacao 
(
      Id_TipoTransacao INT IDENTITY NOT NULL PRIMARY KEY
    , Nome VARCHAR(50) NOT NULL
    , DescricaoAbreviada VARCHAR(60) NOT NULL
    , Descricao VARCHAR(80) NOT NULL
    , FlagCredito BIT NOT NULL
    , FlagSaldoAtual SMALLINT NOT NULL
);

CREATE TABLE Transacao
(
      Id_Transacao INT IDENTITY NOT NULL PRIMARY KEY
    , Id_TipoTransacao INT NOT NULL
    , Id_Conta INT NOT NULL
    , DataHora DATETIME NOT NULL
    , Valor MONEY NOT NULL
    , Historico VARCHAR(60)
    , FOREIGN KEY (Id_Conta) REFERENCES Conta(Id_Conta) --Possivel problema de performance futura, full scan ao inserir
    , FOREIGN KEY (Id_TipoTransacao) REFERENCES TipoTransacao(Id_TipoTransacao) --Possivel problema de performance futura, full scan ao inserir
);

GO

INSERT INTO TipoTransacao VALUES('DepositoConta', 'Deposito em conta', 'C001 - Deposito em conta', 1, 1) --Depositos
INSERT INTO TipoTransacao VALUES('SaqueConta', 'Saque em conta', 'D002 - Saque em conta', 0, -1) --Retiradas
INSERT INTO TipoTransacao VALUES('Pagamento', 'Pagamento', 'D003 - Pagamento', 0, -1) --Pagamentos
INSERT INTO TipoTransacao VALUES('RentabilizarCC', 'Rentabilidade CC diária', 'C004 - Rentabilidade CC diária', 1, 1) --Rentabilizar

SELECT TOP 10 * FROM TipoTransacao WITH(NOLOCK)



/*==================================== CDI ====================================*/
CREATE TABLE TaxaCDI 
(
    Id_TaxaCDI INT IDENTITY NOT NULL PRIMARY KEY
  , Data DATETIME NOT NULL
  , TaxaDia NUMERIC(10,8) NOT NULL
  , Percentual NUMERIC(10,8) NOT NULL
);

CREATE TABLE RendimentoDiarioCC
(
    Id_RendimentoDiarioCC INT IDENTITY NOT NULL PRIMARY KEY
  , Id_Conta INT NOT NULL
  , Id_TaxaCDI INT NOT NULL
  , SaldoAtual MONEY NOT NULL
  , Rendimento MONEY NOT NULL
	, FOREIGN KEY (Id_Conta) REFERENCES Conta(Id_Conta) --Possivel problema de performance futura, full scan ao inserir
	, FOREIGN KEY (Id_TaxaCDI) REFERENCES TaxaCDI(Id_TaxaCDI) --Possivel problema de performance futura, full scan ao inserir
);

--Preecher TaxaCDI


DROP TABLE IF EXISTS #tempData
DECLARE @StartDateTime DATETIME = '2020-07-01'
DECLARE @EndDateTime DATETIME = '2020-12-31';

WITH DateRange(DateData) AS 
(
  SELECT @StartDateTime as Date
  UNION ALL
  SELECT DATEADD(d,1,DateData)
  FROM DateRange 
  WHERE DateData < @EndDateTime
)
SELECT DateData
INTO #tempData
FROM DateRange
OPTION (MAXRECURSION 0)
GO

  
DECLARE @DateData Date;  
DECLARE CDI_cursor CURSOR FOR SELECT DateData FROM #tempData

OPEN CDI_cursor  
FETCH NEXT FROM CDI_cursor INTO @DateData
WHILE @@FETCH_STATUS = 0  
BEGIN  
  INSERT INTO TaxaCDI
  SELECT @DateData, CAST(FLOOR(RAND()*(12000-8400+1))+8400 AS NUMERIC(20,8)) / CAST(1000000 AS NUMERIC(20,8)), CAST(FLOOR(RAND()*(110-80+1))+80 AS NUMERIC(20,8)) / CAST(100 AS NUMERIC(20,8))

  FETCH NEXT FROM CDI_cursor INTO @DateData
END   
CLOSE CDI_cursor;  
DEALLOCATE CDI_cursor;  

SELECT TOP 10 * FROM TaxaCDI