import { BaseQueryArg, createApi } from '@reduxjs/toolkit/query/react';
import baseAuthQuery from '@services/helpers/baseAuthQuery';
import { LoginRequest, LoginResponse, User } from '@serviceTypes/authModels';
import { setUser } from '@slices/user/userSlice';

export const authService = createApi({
  reducerPath: 'authService',
  baseQuery: baseAuthQuery,
  endpoints: (builder) => ({
    login: builder.mutation<LoginResponse, LoginRequest>({
      query: (credentials) => ({
        url: '/auth/login',
        method: 'POST',
        body: credentials,
      }),
    }),
    userAccount: builder.query<User, void>({
      query: (credentials) => ({
        url: '/auth/account',
        method: 'GET',
      }),
    }),
  }),
});

export const {
  useLoginMutation,
  useUserAccountQuery,
  useLazyUserAccountQuery,
} = authService;
