import { Component, inject } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { UserRegistrer } from '../../interfaces/userRegistrer.interface';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-registrer-page',
  templateUrl: './registrer-page.component.html',
  styles: ``
})
export class RegistrerPageComponent {

  public hide : boolean = true;

  // Formato para enlazar al formulario
  public userRegistrerForm = new FormGroup({
    cedula : new FormControl<string>('', { nonNullable: true }),
    nombreUsuario : new FormControl<string>(''),
    correoElectronico: new FormControl<string>(''),
    contraseña : new FormControl<string>('', { nonNullable: true }),
  });

  public get currentUserForm() : UserRegistrer {
    const user = this.userRegistrerForm.value as UserRegistrer
    return user
  }

  private authService = inject(AuthService);
  private router = inject(Router);

  onRegistrer() : void {

    if ( this.userRegistrerForm.invalid ) return;

    this.authService.registrer(this.currentUserForm)
      .subscribe({
        next: () => this.router.navigate(['/auth/']),
        error: ( error ) => {
          console.log(error);
          Swal.fire(`Error: ${error.message}`, error, 'error');
        }
    });
  }
}
