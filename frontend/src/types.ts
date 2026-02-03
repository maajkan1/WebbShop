export interface LoginResponse {
  token: string;
  username: string;
  userId: string;
}

export interface ProductDto {
  id: number;
  productName: string;
  productDescription: string;
  productPrice: number;
  quantity: number;
}

export interface CartItemDto {
  id: number;
  productId: number;
  quantity: number;
  product: ProductDto;
}