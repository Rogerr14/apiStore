import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutPageComponent } from './pages/layout-page/layout-page.component';
import { NewProductPageComponent } from './pages/new-product-page/new-product-page.component';
import { NewCategoryPageComponent } from './pages/new-category-page/new-category-page.component';
import { ListProductPageComponent } from './pages/list-product-page/list-product-page.component';
import { ListCategoryPageComponent } from './pages/list-category-page/list-category-page.component';
import { ProductPageComponent } from './pages/product-page/product-page.component';
import { CategoryPageComponent } from './pages/category-page/category-page.component';
import { BuyPageComponent } from './pages/buy-page/buy-page.component';
import { BillPageComponent } from './pages/bill-page/bill-page.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutPageComponent,
    children: [
      { path: 'new-product', component: NewProductPageComponent },
      { path: 'new-category', component: NewCategoryPageComponent },
      { path: 'edit-product/:id', component: NewProductPageComponent },
      { path: 'edit-category/:id', component: NewCategoryPageComponent },
      { path: '**', component: ListProductPageComponent },
      { path: 'list-categories', component: ListCategoryPageComponent },
      { path: 'product/:id', component: ProductPageComponent },
      { path: 'buy', component: BuyPageComponent },
      { path: 'bill', component: BillPageComponent },
      { path: '**', redirectTo: '**'}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MarketplaceRoutingModule { }
