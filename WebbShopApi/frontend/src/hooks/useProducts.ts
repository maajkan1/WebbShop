import { useCallback, useState } from 'react';
import { getAllProducts, getProductsByCategory } from '../api';
import type { ProductDto } from '../types';

function getErrorMessage(err: unknown) {
  return err instanceof Error ? err.message : 'Unknown error';
}

export function useProducts() {
  const [products, setProducts] = useState<ProductDto[]>([]);
  const [error, setError] = useState('');

  const fetchProducts = useCallback(async (categoryId?: number) => {
    try {
      const items = categoryId
        ? await getProductsByCategory(categoryId)
        : await getAllProducts();

      if (!items || items.length === 0) {
        console.log('Det finns inga produkter');
      }

      setProducts(items);
    } catch (err: unknown) {
      setError(getErrorMessage(err));
    }
  }, []);

  const clearError = useCallback(() => setError(''), []);

  return {
    products,
    fetchProducts,
    error,
    clearError,
  };
}
