import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import LoginForm from '../components/LoginForm';
import { registerUser } from '../api';
import { useAuth } from '../hooks/useAuth';

function LoginPage() {
  const { username, setUsername, login, error } = useAuth();
  const navigate = useNavigate();
  const [isRegistering, setIsRegistering] = useState(false);
  const [email, setEmail] = useState('');
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [registerPassword, setRegisterPassword] = useState('');
  const [registerStatus, setRegisterStatus] = useState('');
  const [registerError, setRegisterError] = useState('');

  const handleSubmit = async (password: string) => {
    await login(password);
    navigate('/');
  };

  return (
    <div>
      <h2>{isRegistering ? 'Skapa konto' : 'Logga in'}</h2>
      {!isRegistering ? (
        <>
          <LoginForm
            username={username}
            onUsernameChange={setUsername}
            onSubmit={handleSubmit}
          />
          {error && <p className="error-text">{error}</p>}
          <button className="button" onClick={() => setIsRegistering(true)}>
            Skapa konto
          </button>
        </>
      ) : (
        <div className="register-form">
          <input
            placeholder="Användarnamn"
            value={username}
            onChange={e => setUsername(e.target.value)}
          />
          <input
            placeholder="E-post"
            value={email}
            onChange={e => setEmail(e.target.value)}
          />
          <input
            placeholder="Förnamn"
            value={firstName}
            onChange={e => setFirstName(e.target.value)}
          />
          <input
            placeholder="Efternamn"
            value={lastName}
            onChange={e => setLastName(e.target.value)}
          />
          <input
            placeholder="Lösenord"
            type="password"
            value={registerPassword}
            onChange={e => setRegisterPassword(e.target.value)}
          />
          <button
            className="button primary"
            onClick={async () => {
              setRegisterError('');
              setRegisterStatus('Skapar konto...');
              try {
                const created = await registerUser({
                  username,
                  password: registerPassword,
                  email,
                  firstName,
                  lastName,
                  createdAt: new Date().toISOString(),
                });
                setRegisterStatus(`Konto skapat för ${created.username}. Logga in.`);
                setIsRegistering(false);
              } catch (err: unknown) {
                setRegisterStatus('');
                setRegisterError(
                  err instanceof Error ? err.message : 'Registrering misslyckades'
                );
              }
            }}
          >
            Skapa konto
          </button>
          <button className="button" onClick={() => setIsRegistering(false)}>
            Tillbaka till inloggning
          </button>
          {registerStatus && <p className="status-text">{registerStatus}</p>}
          {registerError && <p className="error-text">{registerError}</p>}
        </div>
      )}
    </div>
  );
}

export default LoginPage;
