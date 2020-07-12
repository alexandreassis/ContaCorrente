import { Component, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import { ResourceLoader } from '@angular/compiler';


@Component({
  selector: 'app-conta',
  templateUrl: './conta.component.html'
})
export class ContaComponent {
  public contas: ContaDTO[];
  public conta: ContaDTO;
  public tiposTransacoes: TipoTransacaoDTO[];
  public transacaoNova: TransacaoNovaDTO;
  public baseUrl: string;
  public isEnabledCPF: boolean = true;

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }
  
  //SalvarPessoa(pessoa) {
  //  if (this.pessoa.idPessoa > 0) {
  //    this.http.put<PessoaDTO>(this.baseUrl + 'conta', JSON.stringify(this.pessoa), this.httpOptions).subscribe(result => {
  //      this.ResetPessoaTemp();
  //      this.ReloadPessoas();
  //    }, error => {
  //      alert(error.error);
  //    });
  //  }
  //  else {
  //    this.http.post<PessoaDTO>(this.baseUrl + 'conta', JSON.stringify(this.pessoa), this.httpOptions).subscribe(result => {
  //      this.ResetPessoaTemp();
  //      this.ReloadPessoas();
  //    }, error => {
  //      alert(error.error);
  //    });
  //  }
  //}
  ValorValido(num) {
    var regex = /^\d+(?:\.\d{0,2})$/;

    return regex.test(num) && num > 0;
  }

  InserirNovaTransacao() {
    this.transacaoNova.idConta = this.conta.idConta;
    
    if (this.transacaoNova.idTipoTransacao <= 0) {
      alert('Selecione uma transação válida.');
      return;
    }

    if (this.ValorValido(this.transacaoNova.valor) === false) {
      alert('Insira um valor válido. Exemplo: 10.00');
      return;
    }

    var transacao = '{ "idConta": ' + this.transacaoNova.idConta + ', "valor": ' + this.transacaoNova.valor + ', "idTipoTransacao": ' + this.transacaoNova.idTipoTransacao + '}';

    this.http.post<TransacaoDTO>(this.baseUrl + 'transacao', transacao, this.httpOptions).subscribe(result => {
      this.recarregarDadosConta(this.transacaoNova.idConta);
      this.ResetTransacaoNova();
    }, error => {
      alert(error.error);
    });

  }

  LancarRendimentoDiarioCC() {
    this.http.post<TransacaoDTO>(this.baseUrl + 'transacao/LancarRendimentoDiarioCC?idConta=' + this.conta.idConta,  null, this.httpOptions).subscribe(result => {
      this.recarregarDadosConta(this.conta.idConta);
    }, error => {
      alert(error.error);
    });
  }

  ResetContaSelecionada() {
    this.conta = null;
  }
  ResetTransacaoNova() {
    this.transacaoNova = { idConta: 0, valor: 0, idTipoTransacao: 0};
  }


  recarregarDadosConta(idConta: any) {
    if (idConta <= 0)
      return;

    this.http.get<ContaDTO>(this.baseUrl + 'conta/InfosAdicionais?id=' + idConta).subscribe(result => {
      this.conta = result;
    }, error => console.error(error));
  }

  selecionarConta(idConta: any) {
    this.ResetContaSelecionada();
    this.ResetTransacaoNova();

    this.recarregarDadosConta(idConta);
  }

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;

    this.http.get<ContaDTO[]>(this.baseUrl + 'conta').subscribe(result => {
      this.contas = result;
    }, error => console.error(error));

    this.http.get<TipoTransacaoDTO[]>(this.baseUrl + 'transacao/BuscarTipoTransacao').subscribe(result => {
      this.tiposTransacoes = result;
    }, error => console.error(error));
    
    this.ResetContaSelecionada();
    this.ResetTransacaoNova();
  }

}

interface ContaDTO {
  idPessoa: number;
  idConta: number;
  saldoAtual: number;
  pessoa: PessoaDTO;
  transacoes: TransacaoDTO[]
}

interface PessoaDTO {
  idPessoa: number;
  cpf: string;
  nome: string;
}

interface TransacaoDTO {
  idTransacao: number;
  idTipoTransacao: number;
  idConta: number;
  dataHora: string; //DateTime 
  valor: number;
  historico: string;
  tipoTransacao: TipoTransacaoDTO
}

interface TransacaoNovaDTO {
  idTipoTransacao: number;
  idConta: number;
  valor: number;
}

interface TipoTransacaoDTO {
  idTipoTransacao: number;
  nome: string;
  descricaoAbreviada: string;
  descricao: string;
  flagCredito: boolean;
  FlagSaldoAtual: number;
}
