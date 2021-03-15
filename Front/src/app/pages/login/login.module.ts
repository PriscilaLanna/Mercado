

import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';  

import { LoginRoutingModule } from './login-routing.module';
import { LoginComponent } from './login/login.component';
import { SharedModule } from './../../shared/shared.module';

@NgModule({
  declarations: [LoginComponent],
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    LoginRoutingModule
  ],
  
})
export class LoginModule { }
