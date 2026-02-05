import { useCallback, useMemo, useState, type ReactNode } from 'react';
import { login } from '../api';
import type { LoginResult } from '../types';
import { AuthContext, type AuthContextValue } from '../context/authContext';

function getErrorMessage(err: unknown) {
  return err instanceof Error ? err.message : 'Unknown error';
}

export function AuthProvider({ children }: { children: ReactNode }) {
  const [username, setUsername] = useState('');
  const [userId, setUserId] = useState('');
  const [token, setToken] = useState('');
  const [nickname, setNickname] = useState('');
  const [error, setError] = useState('');

  const handleLogin = useCallback(async (password: string) => {
    try {
      const res = (await login(username, password)) as LoginResult;
      setToken(res.token);
      setNickname(res.username); // backend skickar username i login-response
      setUserId(res.userId);
      setError('');
    } catch (err: unknown) {
      setError(getErrorMessage(err));
    }
  }, [username]);

  const handleLogout = useCallback(() => {
    setToken('');
    setUserId('');
    setNickname('');
    setError('');
  }, []);

  const clearError = useCallback(() => setError(''), []);

  const value = useMemo<AuthContextValue>(
    () => ({
      username,
      setUsername,
      userId,
      token,
      nickname,
      error,
      login: handleLogin,
      logout: handleLogout,
      clearError,
    }),
    [
      username,
      userId,
      token,
      nickname,
      error,
      handleLogin,
      handleLogout,
      clearError,
    ]
  );

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}
