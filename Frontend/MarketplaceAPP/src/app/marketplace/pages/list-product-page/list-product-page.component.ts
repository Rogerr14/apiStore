import { Component, OnInit } from '@angular/core';
import { Producto } from '../../interfaces/producto.response.interface';
import { MarketplaceService } from '../../services/marketplace.service';
import { delay } from 'rxjs';

@Component({
  selector: 'app-list-product-page',
  templateUrl: './list-product-page.component.html',
  styles: ``
})
export class ListProductPageComponent implements OnInit {

  public productos? : Producto[] = [];

  public hasLoaded : boolean = false;

  constructor( private productMkService: MarketplaceService ){}

  ngOnInit(): void {
    this.productMkService.getProducts()
    .subscribe( productos => {
      this.productos = productos?.result,
      this.hasLoaded = true
    } );
  }
}
