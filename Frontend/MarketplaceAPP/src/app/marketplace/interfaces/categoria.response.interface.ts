export interface CategoriasResponse {
  message: string;
  result:  Categoria[];
}

export interface CategoriaResponse {
  message: string;
  result:  Categoria;
}

export interface Categoria {
  idCategoria: number;
  nombre:      string;
  estado:      number;
}

export interface CategoriaAdd {
  nombre: string;
}

export interface CategoriaMessageResponse {
  message: string;
  result:  string;
}
