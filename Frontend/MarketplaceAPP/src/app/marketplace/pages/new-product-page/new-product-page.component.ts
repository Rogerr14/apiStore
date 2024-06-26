import { ProductoAdd } from './../../interfaces/producto.response.interface';
import { Component, OnInit, inject } from '@angular/core';
import { MarketplaceService } from '../../services/marketplace.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Producto } from '../../interfaces/producto.response.interface';
import { pipe, switchMap } from 'rxjs';
import { MarketplaceCategoryService } from '../../services/marketplace-category.service';
import { FormControl, FormGroup } from '@angular/forms';
import { Categoria } from '../../interfaces/categoria.response.interface';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-new-product-page',
  templateUrl: './new-product-page.component.html',
  styles: `
    .example-container mat-form-field + mat-form-field {
      margin-left: 8px;
    }

    .example-right-align {
      text-align: right;
    }

    input.example-right-align::-webkit-outer-spin-button,
    input.example-right-align::-webkit-inner-spin-button {
      display: none;
    }

    input.example-right-align {
      -moz-appearance: textfield;
}
  `
})
export class NewProductPageComponent implements OnInit {

  private mkProductService = inject(MarketplaceService);
  private router = inject(Router);
  private activatedRoute = inject(ActivatedRoute);
  private categoryService = inject(MarketplaceCategoryService);

  public imagen? : string;
  public hasLoaded : boolean = false;
  public categorias? : Categoria[] = [];

  // Formato para enlazar al formulario
  public productoForm = new FormGroup({
    idProducto : new FormControl<number | null>(null),
    nombre : new FormControl<string>('', { nonNullable: true }),
    stock : new FormControl<number | null>(null, { nonNullable: true }),
    precioUnitario: new FormControl<number | null>(null, { nonNullable: true }),
    urlImagen : new FormControl<string>('', { nonNullable: true }),
    descripcion : new FormControl<string>('', { nonNullable: true }),
    categoriaIdCategoria : new FormControl<number>(0),
    estado : new FormControl<number>(0),
  });

  public get currentProductForm() : Producto {
    const prod = this.productoForm.value as Producto

    this.imagen = prod.urlImagen;

    return prod
  }


  public get getProductForm() : ProductoAdd {
    const Prod = this.productoForm.value as Producto

    const producto : ProductoAdd = {
      nombre : Prod.nombre,
      stock : Prod.stock,
      precioUnitario : Prod.precioUnitario,
      urlImagen : Prod.urlImagen,
      descripcion : Prod.descripcion,
      idCategoria : Prod.categoriaIdCategoria
    };
    return producto
  }


  ngOnInit(): void {

    if ( !this.router.url.includes('edit')){
      this.categoryService.getCategorias()
      .subscribe( res => {
        this.categorias = res?.result;
      });

      return
    }

    this.activatedRoute.params
      .pipe(
        switchMap( ({ id }) => this.mkProductService.getProductById(id) ),
      ).subscribe( respuesta => {
        if ( !respuesta?.result ) return this.router.navigate(['/marketplace/']);

        this.categoryService.getCategorias()
          .subscribe( res => {
            this.categorias = res?.result;
          });

        this.imagen = respuesta.result.urlImagen;
        this.productoForm.reset( respuesta.result );
        this.hasLoaded = true;
        return;
      });
  }

  onAgregar(): void {

    if (this.productoForm.invalid) return

    this.mkProductService.AddProduct(this.getProductForm)
      .subscribe({
        next: () => this.router.navigate(['/marketplace/']),
        error: ( error ) => {
          Swal.fire('Error', error.messsage, 'error');
        }
      });
  }

  onActualizar( id : number ): void {

    if (this.productoForm.invalid) return

    this.mkProductService.updateProduct(this.getProductForm, id)
      .subscribe({
        next: () => this.router.navigate(['/marketplace/']),
        error: ( error ) => {
          Swal.fire('Error', error.messsage, 'error');
        }
      });
  }

  onDelete( id : number ){
    this.mkProductService.DeleteProduct(id)
      .subscribe({
        next: () => this.router.navigate(['/marketplace/']),
        error: ( error ) => {
          Swal.fire('Error', error.messsage, 'error');
        }
      });
  }
}
