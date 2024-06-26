export interface FacturaCompraResponse {
  message: string;
  result:  FacturaCabezera;
}

export interface FacturaCabezera {
  idCompra:         number;
  usuarioIdUsuario: number;
  total:            number;
  fecha:            Date;
  usuarioCedula:    string;
  detalles:         FacturaDetalle[];
}

export interface FacturaDetalle {
  cantidad:           number;
  total:              number;
  productoIdProducto: number;
  cabezeraIdCompra:   number;
}


export interface CabezeraCompra {
  usuarioIdUsuario: number;
  detalles:         DetalleCompra[];
}

export interface DetalleCompra {
  cantidad:           number;
  productoIdProducto: number;
}

export interface ProductoCarrito {
  idProducto:           number;
  nombre:               string;
  stock:                number;
  precioUnitario:       number;
  categoriaIdCategoria: number;
  estado:               number;
  urlImagen:            string;
  descripcion:          string;
  cantidad:             number;
}
