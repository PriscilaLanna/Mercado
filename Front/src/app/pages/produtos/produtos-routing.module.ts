import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProdutolistComponent } from './produtolist/produtolist.component';
import { ProdutoformComponent } from './produtoform/produtoform.component';


const routes: Routes = [
  { path: '', component: ProdutolistComponent },
  {path:':id/edit',component: ProdutoformComponent },
  {path:'new',component: ProdutoformComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProdutosRoutingModule { }
