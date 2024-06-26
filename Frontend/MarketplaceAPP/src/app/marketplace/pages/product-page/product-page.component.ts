import { Component, OnInit, inject } from '@angular/core';
import { MarketplaceService } from '../../services/marketplace.service';
import { ActivatedRoute, Router } from '@angular/router';
import { switchMap } from 'rxjs';
import { Producto } from '../../interfaces/producto.response.interface';
import { AuthService } from '../../../auth/services/auth.service';
import { ProductoCarrito } from '../../interfaces/compra.interface';
import { CompraService } from '../../services/compra-service.service';

@Component({
  selector: 'app-product-page',
  templateUrl: './product-page.component.html',
  styles: ``
})
export class ProductPageComponent implements OnInit {

  private mkProductService = inject(MarketplaceService);
  private compraService = inject(CompraService);
  private router = inject(Router);
  private activatedRoute = inject(ActivatedRoute);

  public producto? : Producto;
  public hasLoaded : boolean = false;
  public isAuthorize : boolean = false;

  ngOnInit(): void {
    this.activatedRoute.params
      .pipe(
        switchMap( ({ id }) => this.mkProductService.getProductById(id) ),
      ).subscribe( respuesta => {
        if ( !respuesta?.result ) return this.router.navigate(['/marketplace/']);

        this.isAuthorize = this.mkProductService.isAuthorize();
        this.producto = respuesta.result;
        this.hasLoaded = true;
        return;
      });
  }

  addToCart( producto : Producto){

    const prod : ProductoCarrito = {
      idProducto: producto.idProducto,
      nombre: producto.nombre,
      stock: producto.stock,
      precioUnitario: producto.precioUnitario,
      categoriaIdCategoria: producto.categoriaIdCategoria,
      estado: producto.estado,
      urlImagen: producto.urlImagen,
      descripcion: producto.descripcion,
      cantidad: 0
    }

    this.compraService.addProduct( prod );
  }
}
