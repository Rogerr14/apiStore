import { Injectable } from '@angular/core';
import { environments } from '../../../environments/environments';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, catchError, of, throwError } from 'rxjs';
import { ExitoMessageResponse, Producto, ProductoAdd, ProductoResponse, ProductosResponse } from '../interfaces/producto.response.interface';
import { jwtDecode } from 'jwt-decode';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';

@Injectable({
  providedIn: 'root'
})
export class MarketplaceService {

  private baseUrl : string = environments.baseUrl;

  constructor( private http : HttpClient ) { }

  public getProducts() :Observable<ProductosResponse | undefined> {

    const authToken = localStorage.getItem('token');

    if ( !authToken ) return of(undefined);

    const headers = new HttpHeaders()
      .set('Authorization', `Bearer ${ authToken }`);

    return this.http.get<ProductosResponse>(`${this.baseUrl}/api/Producto`, { headers })
      .pipe(
        catchError( err => of(undefined))
      );
  }

  public isAuthorize() : boolean{
    const token = localStorage.getItem('token');

    if (token) {
      // Decodificar el token JWT
      const decodedToken: any = jwtDecode(token);

      // Extraer el rol del token decodificado
      if( decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] == 'Administrador' ) return true;
    }

    return false;
  }

  public getProductById( id : number ) : Observable<ProductoResponse | undefined > {

    const authToken = localStorage.getItem('token');

    if ( !authToken ) return of(undefined);

    const headers = new HttpHeaders()
      .set('Authorization', `Bearer ${ authToken }`);

    return this.http.get<ProductoResponse>(`${ this.baseUrl }/api/Producto/${ id }`, { headers });
  }

  public updateProduct( producto : ProductoAdd, id : number ) : Observable<ExitoMessageResponse | undefined> {

    const authToken = localStorage.getItem('token');

    if ( !authToken ) return of(undefined);

    const headers = new HttpHeaders()
      .set('Authorization', `Bearer ${ authToken }`);

    return this.http.put<ExitoMessageResponse>(`${this.baseUrl}/api/Producto/${ id }`, producto, { headers });
  }

  public AddProduct( producto : ProductoAdd) : Observable<ExitoMessageResponse | undefined> {

    const authToken = localStorage.getItem('token');

    if ( !authToken ) return of(undefined);

    const headers = new HttpHeaders()
      .set('Authorization', `Bearer ${ authToken }`);

    return this.http.post<ExitoMessageResponse>(`${this.baseUrl}/api/Producto`, producto, { headers });
  }

  public DeleteProduct( id : number ) : Observable<ExitoMessageResponse | undefined>{
    const authToken = localStorage.getItem('token');

    if ( !authToken ) return of(undefined);

    const headers = new HttpHeaders()
      .set('Authorization', `Bearer ${ authToken }`);

      return this.http.delete<ExitoMessageResponse>(`${this.baseUrl}/api/Producto/${id}`, { headers });
  }
}
