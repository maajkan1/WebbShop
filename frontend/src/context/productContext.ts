import { createContext } from 'react';
import type { ProductDto } from '../types';

type ProductContextValue = {
  products: ProductDto[];
  fetchProducts: () => Promise<void>;
  error: string;
  clearError: () => void;
};

export const ProductContext = createContext<ProductContextValue | undefined>(undefined);
export type { ProductContextValue };
