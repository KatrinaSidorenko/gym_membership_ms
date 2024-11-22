'use client';

import Dashboard from '@/app/dashboard/[role]/page';
import ProtectedRoute from '@/lib/helpers/protectedRoute';
import Login from '@/app/login/page';

export default function Home() {
  return (
    <ProtectedRoute>
      <Dashboard/>
    </ProtectedRoute>
  )
}
