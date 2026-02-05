import type { CartItemDto } from '../types';
import { formatCurrencySEK } from '../utils/format';

type CartPanelProps = {
  cartItems: CartItemDto[];
  onRemoveItem: (cartItemId: number) => void;
  onClearCart: () => void;
  total: number;
  onCheckout: () => void;
};

function CartPanel({ cartItems, onRemoveItem, onClearCart, total, onCheckout }: CartPanelProps) {
  return (
    <div>
      <div className="cart-actions">
        <button className="button" onClick={onClearCart} disabled={cartItems.length === 0}>
          Töm varukorg
        </button>
        <button className="button primary" onClick={onCheckout} disabled={cartItems.length === 0}>
          Till kassan
        </button>
      </div>
      <ul>
        {cartItems.map(item => (
          <li key={item.id}>
            {item.product.productName} x {item.quantity} ({formatCurrencySEK(item.product.productPrice)})
            <button className="button" onClick={() => onRemoveItem(item.id)}>
              Ta bort
            </button>
          </li>
        ))}
      </ul>
      <div className="cart-total">
        <span>Totalt:</span>
        <strong>{formatCurrencySEK(total)}</strong>
      </div>
    </div>
  );
}

export default CartPanel;
