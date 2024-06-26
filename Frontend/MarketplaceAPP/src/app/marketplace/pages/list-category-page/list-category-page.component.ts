import { Component, OnInit, Output, inject } from '@angular/core';
import { MarketplaceCategoryService } from '../../services/marketplace-category.service';
import { Categoria } from '../../interfaces/categoria.response.interface';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-list-category-page',
  templateUrl: './list-category-page.component.html',
  styles: ``
})
export class ListCategoryPageComponent implements OnInit {

  private categoryService = inject(MarketplaceCategoryService);

  private categoryIdSubscription? : Subscription;

  public categorias? : Categoria[] = [];

  public hasLoaded : boolean = false;

  @Output()
  public category? : Categoria;

  ngOnInit(): void {

    this.categoryService.myCategoriesList$.subscribe( category => {
      if ( category.length == 0 ){

        this.categoryService.getCategorias()
          .subscribe( categoria => {
            this.categorias = categoria?.result;
            this.hasLoaded = true;
          });
      }
    });


  }


}
