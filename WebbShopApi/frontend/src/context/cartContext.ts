import { createContext } from 'react';
import type { CartItemDto } from '../types';

type CartContextValue = {
  cartItems: CartItemDto[];
  fetchCart: (token: string) => Promise<void>;
  removeCartItem: (token: string, cartItemId: number) => Promise<void>;
  clearCart: (token: string) => Promise<void>;
  error: string;
  clearError: () => void;
};

export const CartContext = createContext<CartContextValue | undefined>(undefined);
export type { CartContextValue };
