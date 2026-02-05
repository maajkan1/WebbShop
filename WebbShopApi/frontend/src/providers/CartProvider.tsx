import { useCallback, useMemo, useState, type ReactNode } from 'react';
import { clearCart, getCart, removeCartItem } from '../api';
import { CartContext, type CartContextValue } from '../context/cartContext';
import type { CartItemDto } from '../types';

function getErrorMessage(err: unknown) {
  return err instanceof Error ? err.message : 'Unknown error';
}

export function CartProvider({ children }: { children: ReactNode }) {
  const [cartItems, setCartItems] = useState<CartItemDto[]>([]);
  const [error, setError] = useState('');

  const fetchCart = useCallback(async (token: string) => {
    if (!token) return;
    try {
      const items = await getCart(token);

      if (!items || items.length === 0) {
        console.log('Varukorgen är tom');
      }

      setCartItems(items);
    } catch (err: unknown) {
      setError(getErrorMessage(err));
    }
  }, []);

  const removeItem = useCallback(async (token: string, cartItemId: number) => {
    if (!token) return;
    try {
      await removeCartItem(token, cartItemId);
      setCartItems(items => items.filter(item => item.id !== cartItemId));
    } catch (err: unknown) {
      setError(getErrorMessage(err));
    }
  }, []);

  const clearAll = useCallback(async (token: string) => {
    if (!token) return;
    try {
      await clearCart(token);
      setCartItems([]);
    } catch (err: unknown) {
      setError(getErrorMessage(err));
    }
  }, []);

  const clearError = useCallback(() => setError(''), []);

  const value = useMemo<CartContextValue>(
    () => ({ cartItems, fetchCart, removeCartItem: removeItem, clearCart: clearAll, error, clearError }),
    [cartItems, fetchCart, removeItem, clearAll, error, clearError]
  );

  return <CartContext.Provider value={value}>{children}</CartContext.Provider>;
}
