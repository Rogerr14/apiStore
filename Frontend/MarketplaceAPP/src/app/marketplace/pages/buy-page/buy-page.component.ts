import { Component, OnInit, ViewChild, ViewContainerRef, inject } from '@angular/core';
import { CompraService } from '../../services/compra-service.service';
import { CabezeraCompra, DetalleCompra, FacturaCabezera, FacturaCompraResponse, ProductoCarrito } from '../../interfaces/compra.interface';
import { FacturaComponent } from '../../components/factura/factura.component';
import { AuthService } from '../../../auth/services/auth.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-buy-page',
  templateUrl: './buy-page.component.html',
  styles: ``
})
export class BuyPageComponent implements OnInit {

  private compraService = inject(CompraService);
  private authService = inject(AuthService);
  private router = inject(Router);

  public listProductCart : ProductoCarrito[] = [];

  public hasLoaded : boolean = false;

  private respuesta? : FacturaCabezera;

  myBill$ = this.compraService.myBill$;

  ngOnInit(): void {
    this.listProductCart = this.compraService.currentListProductsCart;
    this.hasLoaded = true;
  }

  private getDetalles() : DetalleCompra[] {
    const detalle : DetalleCompra[] = [];

    for ( var prod of this.listProductCart){
      const detail : DetalleCompra = { cantidad : prod.cantidad, productoIdProducto : prod.idProducto }

      detalle.push(detail);
    }

    return detalle;

  }


  public enviarCompra(){

    const enviarCompra : CabezeraCompra = {
      usuarioIdUsuario : this.authService.getIdFromToken()!,
      detalles : this.getDetalles()
      }

      this.compraService.procesarCompra(enviarCompra)
        .subscribe({
          next: (res) => {
            Swal.fire({
              position: "top-end",
              icon: "success",
              title: res?.message,
              showConfirmButton: false,
              timer: 1500
            })
            this.respuesta = res?.result
            this.compraService.setBill(this.respuesta!)
            this.router.navigate(['/marketplace/bill']);
          },
          error: ( error ) => {
            console.log(error);
            Swal.fire('Error', error.message, 'error');

          }
        });
  }

    // const bill = new FacturaComponent();

    // bill.factura =
}
