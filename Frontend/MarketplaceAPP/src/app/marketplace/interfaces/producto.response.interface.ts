export interface ProductoAdd {
  nombre:         string;
  stock:          number;
  precioUnitario: number;
  urlImagen:      string;
  descripcion:    string;
  idCategoria:    number;
}


export interface ExitoMessageResponse {
  message: string;
  result:  string;
}

export interface ProductoResponse {
  message: string;
  result:  Producto;
}

export interface ProductosResponse {
  message: string;
  result:  Producto[];
}

export interface Producto {
  idProducto:           number;
  nombre:               string;
  stock:                number;
  precioUnitario:       number;
  categoriaIdCategoria: number;
  estado:               number;
  urlImagen:            string;
  descripcion:          string;
}

