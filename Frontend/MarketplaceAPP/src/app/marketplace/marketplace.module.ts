import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MarketplaceRoutingModule } from './marketplace-routing.module';
import { ProductImagePipe } from './pipes/product-image.pipe';
import { LayoutPageComponent } from './pages/layout-page/layout-page.component';
import { NewCategoryPageComponent } from './pages/new-category-page/new-category-page.component';
import { NewProductPageComponent } from './pages/new-product-page/new-product-page.component';
import { ListProductPageComponent } from './pages/list-product-page/list-product-page.component';
import { ListCategoryPageComponent } from './pages/list-category-page/list-category-page.component';
import { BuyPageComponent } from './pages/buy-page/buy-page.component';
import { CardProductComponent } from './components/card-product/card-product.component';
import { CartComponent } from './components/cart/cart.component';
import { CardCategoryComponent } from './components/card-category/card-category.component';
import { MaterialModule } from '../material/material.module';
import { ReactiveFormsModule } from '@angular/forms';
import { ProductPageComponent } from './pages/product-page/product-page.component';
import { CategoryPageComponent } from './pages/category-page/category-page.component';
import { FacturaComponent } from './components/factura/factura.component';
import { BillPageComponent } from './pages/bill-page/bill-page.component';


@NgModule({
  declarations: [
    ProductImagePipe,
    LayoutPageComponent,
    NewCategoryPageComponent,
    NewProductPageComponent,
    ListProductPageComponent,
    ListCategoryPageComponent,
    BuyPageComponent,
    CardProductComponent,
    CartComponent,
    CardCategoryComponent,
    ProductPageComponent,
    CategoryPageComponent,
    FacturaComponent,
    BillPageComponent
  ],
  imports: [
    CommonModule,
    MarketplaceRoutingModule,
    MaterialModule,
    ReactiveFormsModule,
  ]
})
export class MarketplaceModule { }
