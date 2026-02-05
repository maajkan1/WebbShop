import { useEffect, useState } from 'react';
import { Navigate } from 'react-router-dom';
import { getOrderHistory, getOrderHistoryValue } from '../api';
import { useAuth } from '../hooks/useAuth';
import type { OrderDto } from '../types';
import { formatCurrencySEK } from '../utils/format';

function OrderHistoryPage() {
  const { token } = useAuth();
  const [orders, setOrders] = useState<OrderDto[]>([]);
  const [error, setError] = useState('');
  const [totalValue, setTotalValue] = useState<number | null>(null);

  useEffect(() => {
    if (!token) return;

    const loadOrders = async () => {
      try {
        const [history, value] = await Promise.all([
          getOrderHistory(token),
          getOrderHistoryValue(token),
        ]);
        setOrders(history);
        setTotalValue(value);
      } catch (err: unknown) {
        setError(err instanceof Error ? err.message : 'Kunde inte hämta ordrar');
      }
    };

    loadOrders();
  }, [token]);

  if (!token) {
    return <Navigate to="/login" replace />;
  }

  return (
    <section className="order-history">
      <h2>Orderhistorik</h2>
      {error && <p className="error-text">{error}</p>}
      {orders.length === 0 && !error ? <p>Inga ordrar ännu.</p> : null}
      <div className="order-list">
        {orders.map(order => (
          <div key={order.id} className="order-card">
            <div className="order-header">
              <div>Order nr {order.id}</div>
              <div>
                {new Date(order.orderDate).toLocaleString('sv-SE', {
                  dateStyle: 'short',
                  timeStyle: 'short',
                })}
              </div>
            </div>
            <ul className="order-items">
              {order.items.map(item => (
                <li key={`${order.id}-${item.productId}`}>
                  <span>{item.productName}</span>
                  <span>
                    {item.quantity} × {formatCurrencySEK(item.price)}
                  </span>
                </li>
              ))}
            </ul>
          </div>
        ))}
      </div>
      {totalValue !== null ? (
        <div className="order-total">
          <span>Totalt ordervärde</span>
          <strong>{formatCurrencySEK(totalValue)}</strong>
        </div>
      ) : null}
    </section>
  );
}

export default OrderHistoryPage;
