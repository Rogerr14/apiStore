import { Component, Input } from '@angular/core';
import { FacturaCabezera } from '../../interfaces/compra.interface';

@Component({
  selector: 'marketplace-factura',
  templateUrl: './factura.component.html',
  styles: ``
})
export class FacturaComponent {

  public factura? : FacturaCabezera;

  constructor(){}

}
