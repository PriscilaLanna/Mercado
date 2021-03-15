import { AuthService } from './auth.service';
import {ActivatedRouteSnapshot, CanActivate, CanLoad, RouterStateSnapshot, Router} from '@angular/router';
import { Observable } from 'rxjs';
import {take, map} from 'rxjs/operators';
import { Injectable } from '@angular/core';
import {Route} from "@angular/router";

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanLoad,CanActivate{

    constructor(private authService:AuthService, private router:Router){

    }
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
        return this.authService.getLoggedIn().pipe(
            take(1),
            map((isLooged:boolean)=>{
                if(!isLooged){
                    this.router.navigate(['/login']);
                    return false;
                }
                return true;
            })
        )
    }
    canLoad(route: Route, segments: import("@angular/router").UrlSegment[]): Observable<boolean> {
        return this.authService.getLoggedIn().pipe(
            take(1),
            map((isLooged:boolean)=>{
                if(!isLooged){
                    this.router.navigate(['/login']);
                    return false;
                }
                return true;
            })
        )
    }

}