import { Component, OnInit, inject } from '@angular/core';
import { CompraService } from '../../services/compra-service.service';
import { FacturaCabezera, FacturaDetalle } from '../../interfaces/compra.interface';
import { MatTableDataSource } from '@angular/material/table';
import jsPDF from 'jspdf';

@Component({
  selector: 'app-bill-page',
  templateUrl: './bill-page.component.html',
  styles: ``
})
export class BillPageComponent implements OnInit {

  private compraService = inject(CompraService);

  public factura? : FacturaCabezera;

  public displayedColumns: string[] = ['Cantidad', 'Producto', 'Precio Unitario', 'Total'];
  public dataSource = new MatTableDataSource<FacturaDetalle>();

  ngOnInit(): void {
    this.factura = this.compraService.currentBill;
    this.dataSource.data = this.factura!.detalles
  }

  // descargarFactura(){
  //   const pdf = new jsPDF();

  //   const html = document.getElementById("billDownload")!;

  //   pdf.html(html,{ x : 10, y: 10})
  //   console.log(html)

  //   pdf.save(`factura-${this.factura?.idCompra}-${this.factura?.fecha}`);
  // }
}
