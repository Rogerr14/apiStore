import { CompraService } from './../../services/compra-service.service';
import { Categoria } from './../../interfaces/categoria.response.interface';
import { Component, Input, OnInit, inject } from '@angular/core';
import { Producto } from '../../interfaces/producto.response.interface';
import { MarketplaceService } from '../../services/marketplace.service';
import { MarketplaceCategoryService } from '../../services/marketplace-category.service';
import { Router } from '@angular/router';
import { ProductoCarrito } from '../../interfaces/compra.interface';

@Component({
  selector: 'product-card-product',
  templateUrl: './card-product.component.html',
  styles: ``
})
export class CardProductComponent implements OnInit{

  @Input()
  public producto! : Producto;

  public isAuthorize : boolean = false;

  public categoria? : string;

  private productService = inject(MarketplaceService);
  private categoryService = inject(MarketplaceCategoryService);
  private compraService = inject(CompraService);
  private router = inject(Router);

  ngOnInit(): void {
    if (!this.producto) throw Error("Producto field is required");

    this.categoryService.getById(this.producto.categoriaIdCategoria)
      .subscribe(
        res => {
          if (res) {
            this.categoria = res.result.nombre
            return
          }
          this.categoria = 'Indefinida'
        }
      );

    this.isAuthorize = this.productService.isAuthorize();
  }

  onSelect( id : number ): void {
    this.router.navigate([`marketplace/edit-product/${id}`]);
  }

  onDetalle( id : number ): void {
    this.router.navigate([`marketplace/product/${id}`]);
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
