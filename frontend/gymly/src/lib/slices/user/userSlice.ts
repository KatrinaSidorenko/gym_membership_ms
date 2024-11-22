import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { RootState } from '@/lib/store';
import { User } from '@/common/types/authModels';

const initialState: User | undefined = {
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
      if (action.payload) {
        console.log(action.payload);

        state.email = action.payload.email;
        state.id = action.payload.id;
        state.name = action.payload.name;
        state.phone = action.payload.phone;
        state.role = action.payload.role;
      } else {
        return undefined;
      }
    },
  },
});

export const { setUser } = userSlice.actions;

export const selectUser = (state: RootState) => state[name];
