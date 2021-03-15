import { BrowserModule } from '@angular/platform-browser';
import { HomeModule } from './pages/home/home.module';
import { LoginModule } from './pages/login/login.module';
import { NgModule } from '@angular/core';

import { CoreModule } from './core/core.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [  
    CoreModule, 
    LoginModule,
    HomeModule,
    AppRoutingModule,
    BrowserModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
