import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environments } from '../../../environments/environments';
import { Producto } from '../interfaces/producto.response.interface';
import { BehaviorSubject, Observable, catchError, of, throwError } from 'rxjs';
import { CabezeraCompra, FacturaCabezera, FacturaCompraResponse, ProductoCarrito } from '../interfaces/compra.interface';

@Injectable({
  providedIn: 'root'
})
export class CompraService {

  private baseUrl : string = environments.baseUrl;

  // Lista carrito
  private myList : ProductoCarrito[] = [];

  // Carrito observable
  private myCart = new BehaviorSubject<ProductoCarrito[]>([]);
  myCart$ = this.myCart.asObservable();

  private myFactura? : FacturaCabezera;

  private myBill = new BehaviorSubject<FacturaCabezera | undefined>(undefined);
  myBill$ = this.myBill.asObservable();

  private http = inject(HttpClient);

  setBill( factura : FacturaCabezera ){
    this.myFactura = factura;
    this.myBill.next(this.myFactura);
  }


  public get currentBill() : FacturaCabezera | undefined {
    return structuredClone(this.myFactura)
  }


  addProduct( produto : ProductoCarrito ){

    if ( this.myList.length === 0 ){
      produto.cantidad = 1;
      this.myList.push(produto);
      this.myCart.next(this.myList);
    }else{
      const isExist = this.myList.find( item => item.idProducto === produto.idProducto);

      if( isExist ){
        isExist.cantidad = isExist.cantidad + 1;
        this.myCart.next(this.myList);
      }else{
        produto.cantidad = 1;
        this.myList.push(produto);
        this.myCart.next(this.myList);
      }
    }
  }

  deleteProduct(id : number){
    this.myList = this.myList.filter( (prod) => {
      return prod.idProducto != id;
    });
    this.myCart.next(this.myList);
  }

  findProductById(id : number){
    return this.myList.find( (element) =>{
      return element.idProducto === id;
    });
  }

  totalCart(){
    const total = this.myList.reduce(function (acc, product) {
      return acc + (product.cantidad * product.precioUnitario);
    }, 0);

    return total;
  }

  public procesarCompra( cart : CabezeraCompra ) : Observable<FacturaCompraResponse | undefined> {

    const authToken = localStorage.getItem('token');

    if ( !authToken ) return of(undefined);

    const headers = new HttpHeaders()
      .set('Authorization', `Bearer ${ authToken }`);

    return this.http.post<FacturaCompraResponse>(`${ this.baseUrl }/api/Compra`, cart, { headers })
    .pipe(
      catchError( err => throwError( () => err.error))
    );
  }

  public get currentListProductsCart() : ProductoCarrito[] {
    return structuredClone(this.myList);
  }

}
