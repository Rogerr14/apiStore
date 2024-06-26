import { Component, OnInit, inject } from '@angular/core';
import { User } from '../../../auth/interfaces/user';
import { Router } from '@angular/router';
import { AuthService } from '../../../auth/services/auth.service';
import { MarketplaceService } from '../../services/marketplace.service';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { CartComponent } from '../../components/cart/cart.component';

@Component({
  selector: 'app-layout-page',
  templateUrl: './layout-page.component.html',
  styles: ``
})
export class LayoutPageComponent implements OnInit {

  public sideBarItems = [
    { label: 'Marketplace', icon: 'store', url: '/marketplace/'},
    { label: 'Añadir producto', icon: 'add', url: '/marketplace/new-product'},
    { label: 'Añadir categoría', icon: 'add', url: '/marketplace/new-category'},
    { label: 'Ver categorías', icon: 'category', url: '/marketplace/list-categories'}
  ]

  public userName? : string;
  public isAuthorize : boolean = false;

  private router = inject(Router);
  private authService = inject(AuthService);
  private productService = inject(MarketplaceService);
  private _bottomSheet = inject(MatBottomSheet);

  ngOnInit(): void {
    // Obtiene el usuario actual del servicio
    this.userName =  this.authService.getNameFromToken();
    const rol = this.productService.isAuthorize()

    this.isAuthorize = rol;

    if (!rol) {
      this.sideBarItems = this.sideBarItems.filter(
        i => i.label !== 'Añadir producto'
        && i.label !== 'Añadir categoría'
      );
    }
  }

  openBottomSheet(): void {
    this._bottomSheet.open(CartComponent);
  }

  // Cierra sesion y redirige al login
  onLogout(): void {
    this.authService.logout();
    this.router.navigate(['/auth/login'])
  }
}
