import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserLogin } from '../interfaces/userLogin';
import { Observable, catchError, map, tap, throwError } from 'rxjs';
import { environments } from '../../../environments/environments';
import { ResponseToken } from '../interfaces/ResponseToken';
import { jwtDecode } from 'jwt-decode';
import { UserRegistrer } from '../interfaces/userRegistrer.interface';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl : string = environments.baseUrl;

  private userTokenName? : string;

  constructor( private http : HttpClient ) { }


  public get currentUserNameToken() : string | undefined {
    if ( !this.userTokenName ) return undefined

    return structuredClone( this.userTokenName );
  }

  // Logout
  logout(): void {
    localStorage.removeItem('token')
  }

  // Guardar token
  saveToken( token : string ) : void {
    localStorage.setItem( 'token', token );
  }

  // Login
  login( user : UserLogin ) : Observable<boolean> {

    return this.http.post<ResponseToken>(`${ this.baseUrl }/api/Auth/Login`, user)
      .pipe(
        tap( res => {
          const token = res.token;
          if (token) localStorage.setItem('token', token);
        }),
        tap(
          () => this.userTokenName = this.getNameFromToken()
        ),
        map( () => true),

        catchError( err => throwError( () => err.error))
      );
  }

  // Método para obtener el rol del token JWT
  getNameFromToken(): string | undefined {
    const token = localStorage.getItem('token');

    if (token) {
      // Decodificar el token JWT
      const decodedToken: any = jwtDecode(token);

      // Extraer el rol del token decodificado
      return decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
    }

    return undefined;
  }

  registrer( newUser : UserRegistrer ) : Observable<boolean> {
    return this.http.post<UserRegistrer>(`${ this.baseUrl }/api/Auth/Registrer`, newUser)
      .pipe(
        map(
          () => true,
        ),
        catchError( err => throwError( () => err.error ))
      );
  }

   // Método para obtener el rol del token JWT
   getIdFromToken(): number | undefined {
    const token = localStorage.getItem('token');

    if (token) {
      // Decodificar el token JWT
      const decodedToken: any = jwtDecode(token);

      // Extraer el rol del token decodificado
      return decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
    }

    return undefined;
  }
}

