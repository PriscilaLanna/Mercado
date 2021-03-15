import { NgxPaginationModule } from 'ngx-pagination';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Produto } from './../../../models/produtos/produto.model';
import { ProdutoServices } from './../../../services/produto/produto.services';
import { isNullOrUndefined } from 'util';

@Component({
  selector: 'app-produtolist',
  templateUrl: './produtolist.component.html',
  styleUrls: ['./produtolist.component.css']
})
export class ProdutolistComponent implements OnInit {

  page: number = 1;
  limit: number = 10;
  totalItems: number;
  produtos: Produto[] = [];
  
  constructor(private produtoServices: ProdutoServices, private router: Router) { }

  ngOnInit(): void {
    this.loadTotal();
    this.loadProdutos();
  }

  loadTotal(){
    this.produtoServices.getTotal().subscribe(
      resources => {
        this.totalItems = resources
      },
      error => alert('Erro ao carregar o total')
      );
  }

  loadProdutos() {
   
    this.produtoServices.getAll(this.page, this.limit).subscribe(
      resources => {
        this.produtos = resources
      },
      error => alert('Erro ao carregar a lista')
     
    )
  }

  delete(produto: Produto) {
    const mustDelete = confirm('Deseja realmente excluir este item?');

    if (mustDelete) {
      this.produtoServices.delete(produto.id).subscribe(
        () => this.produtos = this.produtos.filter(element => element != produto),
        () => alert("Erro ao tentar excluir!")
      )
    }
  }

  editar(id: number): void {
    this.router.navigate(['/produtos'], { queryParams: { id: id } });

  }

  novoProduto() {
    this.router.navigate(['/new']);
  }

  search(nome: string) {
    this.produtoServices.getSearch(nome, this.page, this.limit).subscribe(
      resources => this.produtos = resources.sort((a, b) => b.id - a.id),
      error => alert('Erro ao carregar a lista')
    )
  }

  pageChanged(event){
    this.page = event;
    this.loadProdutos();
  }
}
