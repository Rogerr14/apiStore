import { Component, Input, OnInit, inject } from '@angular/core';
import { Categoria } from '../../interfaces/categoria.response.interface';
import { MarketplaceService } from '../../services/marketplace.service';
import { MarketplaceCategoryService } from '../../services/marketplace-category.service';
import { Router } from '@angular/router';

@Component({
  selector: 'category-card-category',
  templateUrl: './card-category.component.html',
  styles: ``
})
export class CardCategoryComponent implements OnInit {

  private productService = inject(MarketplaceService);
  private categoryService = inject(MarketplaceCategoryService);
  private router = inject(Router);

  @Input()
  public categoria? : Categoria;

  ngOnInit(): void {
    if (!this.categoria) throw Error("Categoria field is required");
  }

  onSelect(id : number){
    this.categoryService.setCategoryId(id);
  }
}
