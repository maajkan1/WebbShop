import { NavLink, Route, Routes, useNavigate } from 'react-router-dom';
import './App.css';
import LandingPage from './pages/LandingPage';
import LoginPage from './pages/LoginPage';
import CartPage from './pages/CartPage';
import ProfilePage from './pages/ProfilePage';
import ProductPage from './pages/ProductPage';
import OrderHistoryPage from './pages/OrderHistoryPage';
import { useState } from 'react';
import { useAuth } from './hooks/useAuth';

function App() {
  const { token, nickname, logout } = useAuth();
  const loginLabel = token ? nickname || 'Konto' : 'Logga in';
  const loginTarget = token ? '/profile' : '/login';
  const navigate = useNavigate();
  const categories = [
    { id: 0, name: 'Alla' },
    { id: 1, name: 'Kläder' },
    { id: 2, name: 'Accessoarer' },
    { id: 3, name: 'Skor' },
    { id: 4, name: 'Träning' },
    { id: 5, name: 'Elektronik' },
  ];
  const [selectedCategoryId, setSelectedCategoryId] = useState(0);

  return (
    <div className="app-shell">
      <header className="app-header">
        <NavLink to="/" className="app-brand">
          Webbshop
        </NavLink>
        <nav className="app-nav">
          <NavLink to="/" end>
            Hem
          </NavLink>
          <NavLink to={loginTarget}>{loginLabel}</NavLink>
          <NavLink to="/orders">Orderhistorik</NavLink>
          <NavLink to="/cart">Varukorg</NavLink>
          {token ? (
            <button className="button" onClick={logout}>
              Logga ut
            </button>
          ) : null}
        </nav>
      </header>
      <div className="app-content">
        <aside className="app-sidebar">
          <h3>Kategorier</h3>
          <ul className="category-list">
            {categories.map(category => (
              <li key={category.id}>
                <button
                  className={
                    selectedCategoryId === category.id
                      ? 'category-button active'
                      : 'category-button'
                  }
                  onClick={() => {
                    setSelectedCategoryId(category.id);
                    navigate('/');
                  }}
                >
                  {category.name}
                </button>
              </li>
            ))}
          </ul>
        </aside>
        <main className="app-main">
          <Routes>
            <Route
              path="/"
              element={<LandingPage selectedCategoryId={selectedCategoryId} />}
            />
            <Route path="/login" element={<LoginPage />} />
            <Route path="/cart" element={<CartPage />} />
            <Route path="/profile" element={<ProfilePage />} />
            <Route path="/orders" element={<OrderHistoryPage />} />
            <Route path="/products/:productId" element={<ProductPage />} />
          </Routes>
        </main>
      </div>
    </div>
  );
}

export default App;