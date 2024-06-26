import { Component, inject } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { UserLogin } from '../../interfaces/userLogin';
import { AuthService } from '../../services/auth.service';

import Swal from 'sweetalert2'

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styles: ``
})
export class LoginPageComponent {

  public hide : boolean = true;

  // Formato para enlazar al formulario
  public userLoginForm = new FormGroup({
    nombreUsuario : new FormControl<string>(''),
    contraseña : new FormControl<string>('', { nonNullable: true }),
  });

  public get currentUserForm() : UserLogin {
    const user = this.userLoginForm.value as UserLogin
    return user
  }

  private authService = inject(AuthService);
  private router = inject(Router);

  onLogin(){

    if ( this.userLoginForm.invalid ) return;

    this.authService.login( this.currentUserForm )
    .subscribe({
      next: () => this.router.navigate(['/marketplace/']),
      error: ( error ) => {
        console.log(error);
        Swal.fire('Error', error.messsage, 'error');
      }
    });
  }
}
