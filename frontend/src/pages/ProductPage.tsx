import { useEffect, useState } from 'react';
import { Link, useNavigate, useParams } from 'react-router-dom';
import { addToCart, getProduct } from '../api';
import { useAuth } from '../hooks/useAuth';
import type { ProductDto } from '../types';
import { formatCurrencySEK } from '../utils/format';

function ProductPage() {
  const { productId } = useParams();
  const navigate = useNavigate();
  const { token } = useAuth();
  const [product, setProduct] = useState<ProductDto | null>(null);
  const [error, setError] = useState('');
  const [status, setStatus] = useState('');
  const [quantity, setQuantity] = useState(1);

  useEffect(() => {
    const id = Number(productId);
    if (!id || Number.isNaN(id)) {
      return;
    }

    const loadProduct = async () => {
      try {
        const data = await getProduct(id);
        setProduct(data);
      } catch (err: unknown) {
        setError(err instanceof Error ? err.message : 'Failed to load product');
      }
    };

    loadProduct();
  }, [productId]);

  const isInvalidId = !productId || Number.isNaN(Number(productId));

  if (isInvalidId) {
    return (
      <section className="product-detail">
        <Link to="/">Tillbaka till produkter</Link>
        <p className="error-text">Ogiltigt produkt-id</p>
      </section>
    );
  }

  if (error) {
    return (
      <section className="product-detail">
        <Link to="/">Tillbaka till produkter</Link>
        <p className="error-text">{error}</p>
      </section>
    );
  }

  if (!product) {
    return (
      <section className="product-detail">
        <Link to="/">Tillbaka till produkter</Link>
        <p>Laddar...</p>
      </section>
    );
  }

  return (
    <section className="product-detail">
      <Link to="/">Tillbaka till produkter</Link>
      <div className="product-detail-card">
        <h2>{product.productName}</h2>
        <p className="product-desc">{product.productDescription}</p>
        <div className="product-price">{formatCurrencySEK(product.productPrice)}</div>
        <div className="product-actions">
          <label className="product-qty">
            Antal
            <input
              type="number"
              min={1}
              value={quantity}
              onChange={e => setQuantity(Math.max(1, Number(e.target.value)))}
            />
          </label>
          <button
            className="button primary"
            onClick={async () => {
              if (!token) {
                navigate('/login');
                return;
              }
              setError('');
              setStatus('Lägger i varukorg...');
              try {
                await addToCart(product.id, quantity, token);
                setStatus('Lagt i varukorg');
              } catch (err: unknown) {
                setStatus('');
                setError(err instanceof Error ? err.message : 'Kunde inte lägga till');
              }
            }}
          >
            Lägg i varukorg
          </button>
        </div>
        {status && <p className="status-text">{status}</p>}
        {error && <p className="error-text">{error}</p>}
      </div>
    </section>
  );
}

export default ProductPage;
