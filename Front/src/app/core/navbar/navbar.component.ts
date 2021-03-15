import { Component, OnInit } from '@angular/core';
import { AuthService } from './../../services/auth/auth.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(private authService:AuthService,private router:Router) { }

  ngOnInit(): void {
  }

  produtos(){
    this.router.navigate(['/produtos']);
  }
  home(){
    this.router.navigate(['/home']);
  }

  logout(){
    this.authService.logout();
  }

}
