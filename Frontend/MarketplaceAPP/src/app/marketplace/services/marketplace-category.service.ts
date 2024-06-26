import { Categoria, CategoriaAdd, CategoriaMessageResponse, CategoriaResponse, CategoriasResponse } from './../interfaces/categoria.response.interface';
import { BehaviorSubject, Observable, catchError, of, throwError } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environments } from '../../../environments/environments';

@Injectable({
  providedIn: 'root'
})
export class MarketplaceCategoryService {

  private baseUrl = environments.baseUrl;

  private myId? : number;

  private myCategoryId = new BehaviorSubject<number | undefined>(undefined);
  myCategoryId$ = this.myCategoryId.asObservable();

  private categoriesList : Categoria[] = [];

  private myCategoriesList = new BehaviorSubject<Categoria[]>([]);
  myCategoriesList$ = this.myCategoriesList.asObservable();

  constructor( private http: HttpClient ) { }

  setCategoryId( id : number ){
    this.myId = id;
    this.myCategoryId.next(this.myId);
  }

  public get selectedCategory() : number | undefined {
    return structuredClone(this.myId);
  }

  public setCategories( categorias : Categoria) {
    this.categoriesList.push(categorias);
    this.myCategoriesList.next(this.categoriesList);
  }

  public get currentCategories() : Categoria[] {
    return structuredClone(this.categoriesList);
  }


  public getById( id : number ) : Observable<CategoriaResponse | undefined> {

    const authToken = localStorage.getItem('token');

    if ( !authToken ) return of(undefined);

    const headers = new HttpHeaders()
      .set('Authorization', `Bearer ${ authToken }`);

    return this.http.get<CategoriaResponse>(`${ this.baseUrl }/api/Categoria/${ id }`, { headers });
  }

  public getCategorias() : Observable<CategoriasResponse | undefined> {

    const authToken = localStorage.getItem('token');

    if ( !authToken ) return of(undefined);

    const headers = new HttpHeaders()
      .set('Authorization', `Bearer ${ authToken }`);

    return this.http.get<CategoriasResponse>(`${this.baseUrl}/api/Categoria`, { headers })
      .pipe(
        catchError( err => throwError( () => err.error))
    );
  }

  public addCategoria( categoria : CategoriaAdd) : Observable<CategoriaMessageResponse | undefined> {

    const authToken = localStorage.getItem('token');

    if ( !authToken ) return of(undefined);

    const headers = new HttpHeaders()
      .set('Authorization', `Bearer ${ authToken }`);

    return this.http.post<CategoriaMessageResponse>(`${this.baseUrl}/api/Categoria`, categoria, { headers })
      .pipe(
        catchError( err => throwError( () => err.error))
      );
  }

  public updateCategoria (categoria : CategoriaAdd, id : number) : Observable<CategoriaMessageResponse | undefined>{

    const authToken = localStorage.getItem('token');

    if ( !authToken ) return of(undefined);

    const headers = new HttpHeaders()
      .set('Authorization', `Bearer ${ authToken }`);

      return this.http.put<CategoriaMessageResponse>(`${this.baseUrl}/api/Categoria/${id}`, categoria, { headers })
      .pipe(
        catchError( err => throwError( () => err.error))
      );
  }

  public deleteCategoria(id : number) : Observable<CategoriaMessageResponse | undefined>{
    const authToken = localStorage.getItem('token');

    if ( !authToken ) return of(undefined);

    const headers = new HttpHeaders()
      .set('Authorization', `Bearer ${ authToken }`);

      return this.http.delete<CategoriaMessageResponse>(`${this.baseUrl}/api/Categoria/${id}`, { headers })
      .pipe(
        catchError( err => throwError( () => err.error))
      );
  }
}
