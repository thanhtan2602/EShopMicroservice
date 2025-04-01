export interface Product {
  id: string;
  name: string;
  category: string[];
  description: string;
  imageFile: string;
  price: number;
}

// Responses
export interface GetProductsResponse {
  listProduct: Product[];
}
export interface GetProductByIdResponse {
  product: Product;
}

// Request
export interface GetProductsRequest {
  pageNumber: number;
  pageSize: number;
}

export interface CreateProductRequest {
  name: string;
  category: string[];
  description: string;
  imageFile: string;
  price: number;
}

export interface UpdateProductRequest {
  id: string;
  name: string;
  category: string[];
  description: string;
  imageFile: string;
  price: number;
}