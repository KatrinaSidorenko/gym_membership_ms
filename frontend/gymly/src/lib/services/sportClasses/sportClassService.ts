import { createApi } from '@reduxjs/toolkit/query/react';
import baseAuthQuery from '@services/helpers/baseAuthQuery';
import { ExtendedSportClass } from '@/common/types/sportClassModels';

export const sportClassService = createApi({
  reducerPath: 'sportClassService',
  baseQuery: baseAuthQuery,
  endpoints: (builder) => ({
    getAllActiveClasses: builder.query<ExtendedSportClass[], void>({
      query: (credentials) => ({
        url: '/sportclasses/active',
        method: 'GET',
      }),
    }),
  }),
});

export const { useGetAllActiveClassesQuery } = sportClassService;
