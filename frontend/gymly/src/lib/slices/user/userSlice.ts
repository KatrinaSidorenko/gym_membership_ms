import { User } from '@slices/user/userModels';
import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { RootState } from '@/lib/store';

const initialState: User = {
  email: '',
  id: 0,
  name: '',
  phone: '',
  role: null,
};

const name = 'user';

export const userSlice = createSlice({
  name: name,
  initialState,
  reducers: {
    setUser(state, action: PayloadAction<User>) {
      state = action.payload;
    },
  },
});

export const { setUser } = userSlice.actions;

export const selectUser = (state: RootState) => state[name];
