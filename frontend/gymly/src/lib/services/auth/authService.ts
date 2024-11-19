import { createApi } from '@reduxjs/toolkit/query/react';
import baseAuthQuery from '@services/helpers/baseAuthQuery';
import { LoginRequest, LoginResponse } from '@services/auth/authModels';

export const authApi = createApi({
  reducerPath: 'apiAuth',
  baseQuery: baseAuthQuery,
  endpoints: (builder) => ({
    login: builder.mutation<LoginResponse, LoginRequest>({
      query: (credentials) => ({
        url: '/auth/login',
        method: 'POST',
        body: credentials,
      }),
    }),
  }),
});

export const { useLoginMutation } = authApi;
