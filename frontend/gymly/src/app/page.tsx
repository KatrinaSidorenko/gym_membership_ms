'use client';

import { useAuth } from '@/lib/providers/authProvider';
import Login from '@/app/login/page';
import Dashboard from '@/app/dashboard/page';

export default function Home() {
  const { isAuthenticated } = useAuth();
  if (!isAuthenticated) {
    return <Login />;
  }

  return <Dashboard />;
}
