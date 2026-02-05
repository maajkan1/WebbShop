import { useCallback, useMemo, useState, type ReactNode } from 'react';
import { getAllProducts } from '../api';
import { ProductContext, type ProductContextValue } from '../context/productContext';
import type { ProductDto } from '../types';

function getErrorMessage(err: unknown) {
  return err instanceof Error ? err.message : 'Unknown error';
}

export function ProductProvider({ children }: { children: ReactNode }) {
  const [products, setProducts] = useState<ProductDto[]>([]);
  const [error, setError] = useState('');

  const fetchProducts = useCallback(async () => {
    try {
      const items = await getAllProducts();

      if (!items || items.length === 0) {
        console.log('There is no products');
      }

      setProducts(items);
    } catch (err: unknown) {
      setError(getErrorMessage(err));
    }
  }, []);

  const clearError = useCallback(() => setError(''), []);

  const value = useMemo<ProductContextValue>(
    () => ({ products, fetchProducts, error, clearError }),
    [products, fetchProducts, error, clearError]
  );

  return <ProductContext.Provider value={value}>{children}</ProductContext.Provider>;
}