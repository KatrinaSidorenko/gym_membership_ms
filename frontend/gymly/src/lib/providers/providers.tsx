'use client';

import { Provider } from 'react-redux';
import { store } from '../store';
import { NextUIProvider } from '@nextui-org/system';
import { useRouter } from 'next/navigation';
import { AuthProvider } from '@/lib/providers/authProvider';

export function ProvidersComponent({
  children,
}: {
  children: React.ReactNode;
}) {
  const router = useRouter();

  return (
    <NextUIProvider navigate={router.push}>
      <AuthProvider>
        <Provider store={store}>{children}</Provider>
      </AuthProvider>
    </NextUIProvider>
  );
}
