import { createContext } from 'react';

type AuthContextValue = {
  username: string;
  setUsername: (value: string) => void;
  userId: string;
  token: string;
  nickname: string;
  error: string;
  login: (password: string) => Promise<void>;
  logout: () => void;
  clearError: () => void;
};

export const AuthContext = createContext<AuthContextValue | undefined>(undefined);
export type { AuthContextValue };
