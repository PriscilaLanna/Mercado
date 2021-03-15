import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './services/auth/auth.guard';

const routes: Routes = [
  
  { path: 'login', loadChildren: () => import('./pages/login/login.module').then(m => m.LoginModule) },
 
  { path: 'produtos', canLoad:[AuthGuard], canActivate:[AuthGuard],
    loadChildren: () => import('./pages/produtos/produtos.module').then(m => m.ProdutosModule) },
  
  { path: 'home', canLoad:[AuthGuard], canActivate:[AuthGuard], 
    loadChildren: () => import('./pages/home/home.module').then(m => m.HomeModule) }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
