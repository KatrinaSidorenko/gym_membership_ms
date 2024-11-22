import '../styles/globals.css';
import { ProvidersComponent } from '@/lib/providers/providers';

export default function RootLayout({
                                     children,
                                   }: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en" className="dark">
      <body  suppressHydrationWarning>
        <ProvidersComponent>
          {children}
        </ProvidersComponent>
      </body>
    </html>
  );
}
