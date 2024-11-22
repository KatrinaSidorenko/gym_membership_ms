import { NextResponse } from 'next/server';
import type { NextRequest } from 'next/server';
import { useAuth } from '@providers/authProvider';

export function middleware(request: NextRequest) {
  const pathname = request.nextUrl.pathname;
  const { getAccountRole, getToken } = useAuth();
  const token = getToken();
  const role = getAccountRole();

  if (!token || !role){
    return NextResponse.redirect(new URL('/login', request.url));
  }

  if (pathname == '/dashboard'){
    if (role === 'Admin'){
      return NextResponse.redirect(new URL('/dashboard/admin', request.url));
    }
  }

  return NextResponse.next();
}

export const config = {
  matcher: [ '/dashboard', '/dashboard/:path*', '/login'],
};
