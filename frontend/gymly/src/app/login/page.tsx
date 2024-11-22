'use client';

import React, { useState, useEffect } from 'react';
import { Button } from '@nextui-org/button';
import { Card, CardHeader, CardBody } from '@nextui-org/card';
import { Spacer } from '@nextui-org/spacer';
import { Input } from '@nextui-org/input';
import {
  useLazyUserAccountQuery,
  useLoginMutation,
} from '@services/auth/authService';
import { useAuth } from '@providers/authProvider';
import { useRouter } from 'next/navigation';
import { useAppDispatch } from '@hooks/reduxHooks';
import { setUser } from '@slices/user/userSlice';
import { Role } from '@serviceTypes/constants';

const CustomContainer: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  return (
    <div className="flex items-center justify-center min-h-screen w-full p-10">
      <div className="max-w-[400px] w-full bg-white rounded-lg">{children}</div>
    </div>
  );
};

export default function Login() {
  const dispatch = useAppDispatch();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [errorMessage, setErrorMessage] = useState('');
  const [isReady, setIsReady] = useState(false); // To manage client-side rendering
  const [loginMutation] = useLoginMutation();
  const [triggerGetAccount] = useLazyUserAccountQuery();
  const { login, setAccountRole } = useAuth();
  const router = useRouter();

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();

    try {
      console.log('login started');
      const loginResponse = await loginMutation({ email, password }).unwrap();
      console.log('Login successful', loginResponse);
      login(loginResponse.token);


      console.log('getAccount started');
      const accountResponse = await triggerGetAccount().unwrap();
      console.log('Fetched account data:', accountResponse);
      dispatch(setUser(accountResponse));
      setAccountRole(accountResponse.role as Role);

      router.push('/dashboard');
    } catch (error: any) {
      console.error('Error:', error);
      if (error.data?.message) {
        setErrorMessage(error.data.message);
      } else {
        setErrorMessage('An unexpected error occurred.');
      }
    }
  };

  return (
    <CustomContainer>
      <Card className="w-full p-8 shadow-lg rounded-lg">
        <CardHeader className="pb-4">
          <h2 className="text-center w-full font-bold text-black">Login</h2>
        </CardHeader>
        <CardBody>
          <form onSubmit={handleSubmit}>
            <Input
              fullWidth
              label="Email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              type="email"
              required
              className="mb-4"
            />
            <Spacer y={1.5} />
            <Input
              fullWidth
              label="Password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
              className="mb-4"
            />
            <Spacer y={1.5} />
            {errorMessage && (
              <p className="text-red-500 mb-4">{errorMessage}</p>
            )}
            <Button
              type="submit"
              fullWidth
              className="bg-blue-500 hover:bg-blue-600 text-white"
            >
              Login
            </Button>
          </form>
        </CardBody>
      </Card>
    </CustomContainer>
  );
}
