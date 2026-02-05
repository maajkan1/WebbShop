import type {
  LoginResult,
  CartItemDto,
  ProductDto,
  UserProfileDto,
  AddCartItemDto,
  AddToCartRequest,
  OrderDto,
  RegisterDto,
} from './types';

const BASE_URL = 'http://localhost:5000';

export async function login(username: string, password: string): Promise<LoginResult> {
  const res = await fetch(`${BASE_URL}/user/login`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ username, password }),
  });
  if (!res.ok) throw new Error('Inloggning misslyckades');
  return res.json();
}

export async function registerUser(payload: RegisterDto): Promise<UserProfileDto> {
  const res = await fetch(`${BASE_URL}/user/register`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(payload),
  });
  if (!res.ok) throw new Error('Registrering misslyckades');
  return res.json();
}

// Nu skickar vi bara token i header, ingen userId
export async function getCart(token: string): Promise<CartItemDto[]> {
  const res = await fetch(`${BASE_URL}/cart/items`, { // OBS: /cart/items, ingen userId
    headers: { Authorization: `Bearer ${token}` },
  });
  if (!res.ok) throw new Error('Kunde inte hämta varukorg');
  return res.json();
}

export async function removeCartItem(
  token: string,
  cartItemId: number
): Promise<void> {
  const res = await fetch(`${BASE_URL}/cart/items/${cartItemId}`, {
    method: 'DELETE',
    headers: { Authorization: `Bearer ${token}` },
  });
  if (!res.ok) throw new Error('Kunde inte ta bort vara');
}

export async function clearCart(token: string): Promise<void> {
  const res = await fetch(`${BASE_URL}/cart/items`, {
    method: 'DELETE',
    headers: { Authorization: `Bearer ${token}` },
  });
  if (!res.ok) throw new Error('Kunde inte tömma varukorg');
}

export async function getAllProducts(): Promise<ProductDto[]> {
  const res = await fetch(`${BASE_URL}/products`, {
  });
  if (!res.ok) throw new Error('Kunde inte hämta produkter');
  return res.json();
}

export async function getProductsByCategory(categoryId: number): Promise<ProductDto[]> {
  const res = await fetch(`${BASE_URL}/products/category/${categoryId}`);
  if (!res.ok) throw new Error('Kunde inte hämta produkter för kategori');
  return res.json();
}

export async function getProduct(productId: number): Promise<ProductDto> {
  const res = await fetch(`${BASE_URL}/products/${productId}`);
  if (!res.ok) throw new Error('Kunde inte hämta produkt');
  return res.json();
}

export async function addToCart(
  productId: number,
  quantity: number,
  token: string
): Promise<AddCartItemDto> {
  const payload: AddToCartRequest = { quantity };
  const res = await fetch(`${BASE_URL}/products/${productId}/add-to-cart`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify(payload),
  });
  if (!res.ok) throw new Error('Kunde inte lägga i varukorg');
  return res.json();
}

export async function getUser(userId: string): Promise<UserProfileDto> {
  const res = await fetch(`${BASE_URL}/user/${userId}`);
  if (!res.ok) throw new Error('Kunde inte hämta användarprofil');
  return res.json();
}

export async function updateProfile(
  token: string,
  userId: string,
  profile: UserProfileDto
): Promise<UserProfileDto> {
  const res = await fetch(
    `${BASE_URL}/user/update-profile?userId=${encodeURIComponent(userId)}`,
    {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify(profile),
    }
  );
  if (!res.ok) throw new Error('Failed to update profile');
  return res.json();
}

export async function createOrder(token: string): Promise<number> {
  const res = await fetch(`${BASE_URL}/orders/create-order`, {
    method: 'POST',
    headers: { Authorization: `Bearer ${token}` },
  });
  if (!res.ok) throw new Error('Kunde inte skapa order');
  return res.json();
}

export async function getOrderHistory(token: string): Promise<OrderDto[]> {
  const res = await fetch(`${BASE_URL}/orders/order-history`, {
    headers: { Authorization: `Bearer ${token}` },
  });
  if (!res.ok) throw new Error('Kunde inte hämta orderhistorik');
  return res.json();
}

export async function getOrderHistoryValue(token: string): Promise<number> {
  const res = await fetch(`${BASE_URL}/orders/order-history/value`, {
    headers: { Authorization: `Bearer ${token}` },
  });
  if (!res.ok) throw new Error('Kunde inte hämta ordervärde');
  return res.json();
}