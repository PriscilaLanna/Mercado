import { RouterModule } from '@angular/router';
import { DinheiroPipe } from './pipe/dinheiro.pipe';
import { ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PageHeaderComponent } from './components/page-header/page-header.component';
import { ErrorMessageComponent } from './components/error-message/error-message.component';


@NgModule({
  declarations: [PageHeaderComponent, ErrorMessageComponent, DinheiroPipe],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule
  ],
  exports: [
    PageHeaderComponent,
    ErrorMessageComponent,
    DinheiroPipe
  ]
})
export class SharedModule { }
