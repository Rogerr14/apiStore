import { Component, Inject, Input, inject } from '@angular/core';
import { MatBottomSheetRef } from '@angular/material/bottom-sheet';
import { DetalleCompra } from '../../interfaces/compra.interface';
import { Producto } from '../../interfaces/producto.response.interface';
import { CompraService } from '../../services/compra-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styles: `
    .flex-container {
      display: flex;
      align-items: center;
    }

    .flex-item {
      flex: 1;
      margin-right: 5px; /* Ajusta el margen según sea necesario */
      margin-left: 5px;
    }

    .flex-item-right {
      flex : 1;
    }
  `
})
export class CartComponent {

  // @Input()
  // public listaProductosCarrito : Producto[] = [];

  // public get detalles() : DetalleCompra[] {

  //   const detallesProductos : DetalleCompra[] = [];

  //   // for ( var producto of this.listaProductosCarrito){
  //   //   const detalle : DetalleCompra = { }
  //   // }

  //   return []
  // };

  private _bottomSheetRef = inject(MatBottomSheetRef<CartComponent>);
  private compraServie = inject(CompraService);
  private router = inject(Router);

  myCart$ = this.compraServie.myCart$;

  totalProduct(price : number, units :number){
    return price * units;
  }

  deleteProduct(id : number){
    this.compraServie.deleteProduct(id);
  }

  updateCantidad( operation : string, id : number ){
    const product = this.compraServie.findProductById(id)

    if (product){
      if ( operation === 'minus' && product.cantidad > 0){
        product.cantidad = product.cantidad - 1;
      }

      if (operation === 'add') {
        product.cantidad = product.cantidad + 1;
        return;
      }

      if (product.cantidad === 0 ){
        this.deleteProduct(product.idProducto);
        return;
      }
    }
  }

  totalCart(){
    const result = this.compraServie.totalCart();
    return result
  }

  goToCart(){
    if (this.compraServie.currentListProductsCart.length > 0){
      this.router.navigate(['/marketplace/buy']);
      this._bottomSheetRef.dismiss();
    }
  }

  // openLink(event: MouseEvent): void {
  //   this._bottomSheetRef.dismiss();
  //   event.preventDefault();
  // }
}
