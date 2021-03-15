import { element } from 'protractor';
import { Injector, Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";

import { Observable, throwError } from "rxjs";
import { map, catchError } from "rxjs/operators";

import { Produto } from './../../models/produtos/produto.model';

@Injectable({ providedIn: 'root' })
export class ProdutoServices {

    private apiPath: string = "https://localhost:44392/api/produto"

    constructor(protected http: HttpClient) { }

    getTotal(): Observable<number> {
        const url = `${this.apiPath}`;
        var produtos: Produto[] = [];

        return this.http.get(url).pipe(
            map( teste => {
               produtos = Object.assign(teste);
               return produtos.length;
            }),
            catchError(this.handleError)
        )
    }

    getAll(page: number, limit: number): Observable<Produto[]> {
        const url = `${this.apiPath}/${page}/${limit}`;

        return this.http.get(url).pipe(
            map(this.jsonDataToResources.bind(this)),
            catchError(this.handleError)
        )
    }

    getSearch(nome: string, page: number, limit: number): Observable<Produto[]> {
        const url = `${this.apiPath}/${nome}/${page}/${limit}`;

        return this.http.get(url).pipe(
            map(this.jsonDataToResources.bind(this)),
            catchError(this.handleError)
        )
    }

    getById(id: number): Observable<Produto> {
        const url = `${this.apiPath}/${id}`;

        return this.http.get(url).pipe(
            map(this.jsonDataToResource.bind(this)),
            catchError(this.handleError)
        )
    }

    create(resource: Produto): Observable<Produto> {
        return this.http.post(this.apiPath, resource).pipe(
            map(this.jsonDataToResource.bind(this)),
            catchError(this.handleError)
        )
    }

    update(resource: Produto): Observable<Produto> {
        const url = `${this.apiPath}`;

        return this.http.put(url, resource).pipe(
            map(() => resource),
            catchError(this.handleError)
        )
    }

    delete(id: number): Observable<any> {
        const url = `${this.apiPath}/${id}`;

        return this.http.delete(url).pipe(
            map(() => null),
            catchError(this.handleError)
        )
    }

    protected jsonDataToResources(jsonData: any[]): Produto[] {
        const resources: Produto[] = [];
        jsonData.forEach(
            element => resources.push(Object.assign(element))
        );
        return resources;
    }

    protected jsonDataToResource(jsonData: any): Produto {
        return Object.assign(jsonData);
    }

    protected handleError(error: any): Observable<any> {
        console.log("ERRO NA REQUISIÇÃO => ", error);
        return throwError(error);
    }

}