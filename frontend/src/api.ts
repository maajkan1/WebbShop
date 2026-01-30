import type { LoginResponse, CartItemDto } from './types';

const BASE_URL = 'http://localhost:5044';

export async function login(username: string, password: string): Promise<LoginResponse> {
  const res = await fetch(`${BASE_URL}/user/login`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ username, password }),
  });
  if (!res.ok) throw new Error('Login failed');
  return res.json();
}

// Nu skickar vi bara token i header, ingen userId
export async function getCart(token: string): Promise<CartItemDto[]> {
  const res = await fetch(`${BASE_URL}/cart/items`, { // OBS: /cart/items, ingen userId
    headers: { Authorization: `Bearer ${token}` },
  });
  if (!res.ok) throw new Error('Failed to fetch cart');
  return res.json();
}