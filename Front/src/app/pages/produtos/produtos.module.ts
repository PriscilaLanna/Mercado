import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProdutosRoutingModule } from './produtos-routing.module';
import { ProdutoformComponent } from './produtoform/produtoform.component';
import { ProdutolistComponent } from './produtolist/produtolist.component';
import { CoreModule } from 'src/app/core/core.module';
import { NgxPaginationModule } from 'ngx-pagination';


@NgModule({
  declarations: [ProdutoformComponent, ProdutolistComponent],
  imports: [
    CoreModule,
    CommonModule,
    ProdutosRoutingModule,
    SharedModule,
    ReactiveFormsModule,
    FormsModule,
    NgxPaginationModule
  ]
})
export class ProdutosModule { }
