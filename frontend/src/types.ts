export interface LoginDto {
  username: string;
  password: string;
}

export interface LoginResult {
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

export interface UserProfileDto {
  id: string;
  username: string;
  email: string;
  firstName: string;
  lastName: string;
  createdAt: string;
}

export interface UpdateProfileDto {
  email: string;
  firstName: string;
  lastName: string;
}

export interface AddCartItemDto {
  cartId: number;
  productId: number;
  quantity: number;
}

export interface AddToCartRequest {
  quantity: number;
}

export interface OrderItemDto {
  productId: number;
  productName: string;
  quantity: number;
  price: number;
  rowTotal: number;
}

export interface OrderDto {
  id: number;
  orderDate: string;
  userId: string;
  userName: string;
  items: OrderItemDto[];
  totalPrice: number;
}

export interface RegisterDto {
  username: string;
  password: string;
  email: string;
  firstName: string;
  lastName: string;
  createdAt: string;
}