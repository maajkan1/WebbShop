import { useState } from 'react';

type LoginFormProps = {
  username: string;
  onUsernameChange: (value: string) => void;
  onSubmit: (password: string) => void;
};

function LoginForm({ username, onUsernameChange, onSubmit }: LoginFormProps) {
  const [password, setPassword] = useState('');

  const handleSubmit = () => {
    onSubmit(password);
    setPassword('');
  };

  return (
    <div className="auth-form">
      <input
        className="auth-input"
        placeholder="Användarnamn"
        value={username}
        onChange={e => onUsernameChange(e.target.value)}
      />
      <input
        className="auth-input"
        placeholder="Lösenord"
        type="password"
        value={password}
        onChange={e => setPassword(e.target.value)}
      />
      <button className="button primary" onClick={handleSubmit}>
        Logga in
      </button>
    </div>
  );
}

export default LoginForm;
