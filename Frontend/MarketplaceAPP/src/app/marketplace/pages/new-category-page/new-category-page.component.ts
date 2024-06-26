import { Component, OnDestroy, OnInit, inject } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Categoria, CategoriaAdd } from '../../interfaces/categoria.response.interface';
import { ActivatedRoute, Router } from '@angular/router';
import { MarketplaceCategoryService } from '../../services/marketplace-category.service';
import { Subscription, switchMap } from 'rxjs';
import Swal from 'sweetalert2';

@Component({
  selector: 'category-new-category-page',
  templateUrl: './new-category-page.component.html',
  styles: ``
})
export class NewCategoryPageComponent implements OnInit, OnDestroy {

  private router = inject(Router);
  private categoryService = inject(MarketplaceCategoryService);
  private activatedRoute = inject(ActivatedRoute);
  private categoryIdSubscription? : Subscription;

  public hasLoaded : boolean = false;
  public categoria? : Categoria;

  // Formato para enlazar al formulario
  public categoryForm = new FormGroup({
    idProducto : new FormControl<number | null>(null),
    nombre : new FormControl<string>('', { nonNullable: true }),
    estado : new FormControl<number | null>(null),
  });

  public get currentCategoryForm() : Categoria {
    const category = this.categoryForm.value as Categoria
    category.idCategoria
    return category
  }

  public get getCategoryForm() : CategoriaAdd {
    const Prod = this.categoryForm.value as Categoria

    const producto : CategoriaAdd = {
      nombre : Prod.nombre,
    };
    return producto
  }

  ngOnDestroy(): void {
    // Desuscribirse del observable cuando el componente se destruye
    this.categoryIdSubscription?.unsubscribe();
  }

  ngOnInit(): void {

    // Suscribirse al observable para detectar cambios en la categoría seleccionada
    this.categoryIdSubscription = this.categoryService.myCategoryId$.subscribe(id => {
      if (id !== undefined) {
        // Lógica para cargar la categoría según el ID
        this.categoryService.getById( id )
        .subscribe( respuesta => {
            if ( respuesta?.result ) {
              this.categoria = respuesta.result;
              this.categoryForm.reset( respuesta.result );
              this.hasLoaded = true;
            }
          });
      }
    });
  }

  onClear(){
    this.categoryForm.reset();
    this.hasLoaded = false;
  }

  onAgregar(){
    if (this.categoryForm.invalid) return

    this.categoryService.addCategoria(this.getCategoryForm)
      .subscribe({
        next: (res) => {
          this.onClear()
          Swal.fire({
            position: "top-end",
            icon: "success",
            title: res?.message,
            showConfirmButton: false,
            timer: 1500
          })
        },
        error: ( error ) => {
          Swal.fire('Error', error.message, 'error');
        }
      });

      this.categoryService.setCategories(this.currentCategoryForm);
  }

  onActualizar(id : number){
    if (this.categoryForm.invalid) return

    this.categoryService.updateCategoria(this.currentCategoryForm, id)
      .subscribe({
        next: (res) => {
          this.onClear()
          Swal.fire({
            position: "top-end",
            icon: "success",
            title: res?.message,
            showConfirmButton: false,
            timer: 1500
          })
        },
        error: ( error ) => {
          Swal.fire('Error', error.message, 'error');
        }
      });
  }

  onDelete(id : number){
    this.categoryService.deleteCategoria(id)
      .subscribe({
        next: (res) => {
          this.onClear()
          Swal.fire({
            position: "top-end",
            icon: "success",
            title: res?.message,
            showConfirmButton: false,
            timer: 1500
          })
        },
        error: ( error ) => {
          Swal.fire('Error', error.message, 'error');
        }
      });
  }
}
