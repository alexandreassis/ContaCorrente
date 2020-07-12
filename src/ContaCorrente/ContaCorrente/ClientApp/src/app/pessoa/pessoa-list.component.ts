import { Component, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import { ResourceLoader } from '@angular/compiler';


@Component({
  selector: 'app-pessoa-list',
  templateUrl: './pessoa-list.component.html'
})
export class PessoaListComponent {
  public pessoas: PessoaDTO[];
  public pessoa: PessoaDTO;
  public baseUrl: string;
  public isEnabledCPF: boolean = true;

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  ReloadPessoas() {
    this.http.get<PessoaDTO[]>(this.baseUrl + 'pessoa').subscribe(result => {
      this.pessoas = result;
    }, error => console.error(error));
  }

  EditarPessoa(pessoa) {
    this.isEnabledCPF = false;
    this.pessoa = { idPessoa: pessoa.idPessoa, cpf: pessoa.cpf, nome: pessoa.nome };
  }

  SalvarPessoa(pessoa) {
    if (this.pessoa.idPessoa > 0) {
      this.http.put<PessoaDTO>(this.baseUrl + 'pessoa', JSON.stringify(this.pessoa), this.httpOptions).subscribe(result => {
        this.ResetPessoaTemp();
        this.ReloadPessoas();
      }, error => {
        alert(error.error);
      });
    }
    else {
      this.http.post<PessoaDTO>(this.baseUrl + 'pessoa', JSON.stringify(this.pessoa), this.httpOptions).subscribe(result => {
        this.ResetPessoaTemp();
        this.ReloadPessoas();
      }, error => {
        alert(error.error);
      });
    }
  }

  ResetPessoaTemp() {
    this.pessoa = { idPessoa: 0, cpf: '', nome: '' };
    this.isEnabledCPF = true;
  }

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;

    this.ReloadPessoas();
    this.ResetPessoaTemp();
  }
}

interface PessoaDTO {
  idPessoa: number;
  cpf: string;
  nome: string;
}
