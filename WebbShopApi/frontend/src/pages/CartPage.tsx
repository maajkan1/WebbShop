import { useEffect, useState } from 'react';
import { Navigate } from 'react-router-dom';
import CartPanel from '../components/CartPanel';
import { createOrder } from '../api';
import { useAuth } from '../hooks/useAuth';
import { useCart } from '../hooks/useCart';

function CartPage() {
  const { token, error: authError } = useAuth();
  const { cartItems, fetchCart, removeCartItem, clearCart, error: cartError } = useCart();
  const [status, setStatus] = useState('');
  const [checkoutError, setCheckoutError] = useState('');

  const total = cartItems.reduce(
    (sum, item) => sum + item.product.productPrice * item.quantity,
    0
  );

  useEffect(() => {
    if (!token) return;
    fetchCart(token);
  }, [token, fetchCart]);

  if (!token) {
    return <Navigate to="/login" replace />;
  }

  return (
    <div>
      <h2>Varukorg</h2>
      <CartPanel
        cartItems={cartItems}
        onRemoveItem={cartItemId => removeCartItem(token, cartItemId)}
        onClearCart={() => clearCart(token)}
        total={total}
        onCheckout={async () => {
          if (!token) return;
          setCheckoutError('');
          setStatus('Behandlar betalning...');
          try {
            await createOrder(token);
            await fetchCart(token);
            setStatus('Order skapad');
          } catch (err: unknown) {
            setStatus('');
            setCheckoutError(err instanceof Error ? err.message : 'Kassan misslyckades');
          }
        }}
      />
      {(authError || cartError) && <p className="error-text">{authError || cartError}</p>}
      {checkoutError && <p className="error-text">{checkoutError}</p>}
      {status && <p className="status-text">{status}</p>}
    </div>
  );
}

export default CartPage;
