'use client';

import { configureStore, createListenerMiddleware } from '@reduxjs/toolkit';
import { authService } from '@services/auth/authService';
import { userSlice } from '@slices/user/userSlice';
import { sportClassService } from '@services/sportClasses/sportClassService';
import { sportClassSlice } from '@slices/sportClass/sportClassSlice';

const listenerMiddleware = createListenerMiddleware();

export const store = configureStore({
  reducer: {
    [authService.reducerPath]: authService.reducer,
    [sportClassService.reducerPath]: sportClassService.reducer,
    [userSlice.name]: userSlice.reducer,
    [sportClassSlice.name]: sportClassSlice.reducer,
  },
  devTools: process.env.NODE_ENV !== 'production',
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware()
      .prepend(listenerMiddleware.middleware)
      .concat(authService.middleware)
      .concat(sportClassService.middleware),
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
