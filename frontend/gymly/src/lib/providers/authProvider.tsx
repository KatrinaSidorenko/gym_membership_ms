'use client';

import React, { createContext, useContext, useEffect, useState } from 'react';
import { User } from '@serviceTypes/authModels';
import { Role } from '@serviceTypes/constants';

interface AuthContextType {
  isAuthenticated: boolean;
  login: (token: string) => void;
  logout: () => void;
  getToken: () => string | null;
  getAccountRole: () => Role;
  setAccountRole: (role: Role) => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);

  useEffect(() => {
    const token = localStorage.getItem('token');
    if (token) {
      setIsAuthenticated(true);
    }
  }, []);

  const login = (token: string) => {
    localStorage.setItem('token', token);
    setIsAuthenticated(true);
  };

  const logout = () => {
    localStorage.removeItem('token');
    setIsAuthenticated(false);
  };

  const getToken = () => {
    return localStorage.getItem('token');
  };

  const setAccountRole = (role: Role) => {
    localStorage.setItem('role', role);
  }

  const getAccountRole = () => {
    return localStorage.getItem('role') as Role;
  }


  return (
    <AuthContext.Provider value={{ isAuthenticated, login, logout, getToken, setAccountRole, getAccountRole }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (context === undefined) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
};
