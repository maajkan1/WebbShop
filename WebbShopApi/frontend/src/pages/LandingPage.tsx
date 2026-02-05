import { useEffect } from 'react';
import { Link } from 'react-router-dom';
import { useProducts } from '../hooks/useProducts';
import { formatCurrencySEK } from '../utils/format';

type LandingPageProps = {
  selectedCategoryId: number;
};

function LandingPage({ selectedCategoryId }: LandingPageProps) {
  const { products, fetchProducts, error } = useProducts();

  useEffect(() => {
    fetchProducts(selectedCategoryId === 0 ? undefined : selectedCategoryId);
  }, [fetchProducts, selectedCategoryId]);

  return (
    <section className="landing">
      <div className="landing-content">
        <h1>Välkommen till Webbshop</h1>
        <p>Handla utvalda produkter, hantera din varukorg och gå snabbt till kassan.</p>
        <div className="product-list">
          <h3>Produkter</h3>
          {error && <p className="error-text">{error}</p>}
          <div className="product-grid">
            {products.map(product => (
              <Link
                key={product.id}
                className="product-card"
                to={`/products/${product.id}`}
              >
                <div className="product-name">{product.productName}</div>
                <div className="product-desc">{product.productDescription}</div>
                <div className="product-price">
                  {formatCurrencySEK(product.productPrice)}
                </div>
              </Link>
            ))}
          </div>
        </div>
      </div>
    </section>
  );
}

export default LandingPage;
