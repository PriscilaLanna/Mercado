import { ReturnAuth } from './../../models/auth/returnauth.model';
import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import toastr from "toastr";

import { User } from './../../models/auth/user.model';

@Injectable({ providedIn: 'root' })
export class AuthService {

    private loggedIn = new BehaviorSubject<boolean>(false);
    private pathAuth: string = 'https://dev.sitemercado.com.br/api/login';

    getLoggedIn(): Observable<boolean> {
        return this.loggedIn.asObservable();
    }

    constructor(private router: Router, private client: HttpClient) {

    }

    private getHeaders(user:User):HttpHeaders{
        return new HttpHeaders({
            'Authorization': 'Basic ' + user.userName + ':' + user.password,
            'Content-Type': 'application/json',
            'charset': 'UTF-8'
        });   
    }

    login(user: User) {
        if (user.userName != '' && user.password != '') {
            
            var headers = this.getHeaders(user);
            this.client.post<ReturnAuth>(this.pathAuth, user, { headers: headers }).subscribe(authenticated => {
               
                if (authenticated.success) {
                    this.loggedIn.next(true);
                    this.router.navigate(['/home']);
                } else {
                    toastr.error("Usuário ou senha inválida!");
                }
            });
        }
    }
    logout(): void {
        this.loggedIn.next(false);
        this.router.navigate(['/login']);
    }

}