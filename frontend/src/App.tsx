import { useState } from 'react';
import { login, getCart } from './api';
import type { CartItemDto } from './types';

function App() {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [token, setToken] = useState('');
  const [nickname, setNickname] = useState('');
  const [cartItems, setCartItems] = useState<CartItemDto[]>([]);
  const [error, setError] = useState('');

const handleLogin = async () => {
  try {
    const res = await login(username, password);
    setToken(res.token);
    setNickname(res.username); // backend skickar username i login-response
    setError('');
  } catch (err: any) {
    setError(err.message);
  }
};

const fetchCart = async () => {
  if (!token) return;
  try {
    const items = await getCart(token);

     if (!items ||items.length == 0) {
      console.log("Cart is empty");
     }

    setCartItems(items);
  } catch (err: any) {
    setError(err.message);
  }
};

  return (
    <div style={{ padding: 20 }}>
      <h1>Test Login & Cart</h1>
      {!token ? (
        <div>
          <input
            placeholder="Username"
            value={username}
            onChange={e => setUsername(e.target.value)}
          />
          <input
            placeholder="Password"
            type="password"
            value={password}
            onChange={e => setPassword(e.target.value)}
          />
          <button onClick={handleLogin}>Login</button>
        </div>
      ) : (
        <div>
          <p>Logged in as: {nickname}</p>
          <button onClick={fetchCart}>Fetch Cart</button>
          <ul>
            {cartItems.map(item => (
              <li key={item.id}>
                {item.product.productName} x {item.quantity} (${item.product.productPrice}
                )
              </li>
            ))}
          </ul>
        </div>
      )}
      {error && <p style={{ color: 'red' }}>{error}</p>}
    </div>
  );
}

export default App;