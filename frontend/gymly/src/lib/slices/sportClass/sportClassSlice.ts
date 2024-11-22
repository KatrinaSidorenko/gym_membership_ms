import { SportClass, SportClasses } from '@/common/types/sportClassModels';
import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { RootState } from '@/lib/store';

interface InitialState {
  sportClasses: SportClasses | [];
}
const initialState: InitialState = {
  sportClasses: [],
};

const name = 'sportClasses';

export const sportClassSlice = createSlice({
  name: name,
  initialState,
  reducers: {
    setSportClasses(state, action: PayloadAction<SportClasses>) {
      state.sportClasses = action.payload;
    },
  },
});

export const { setSportClasses } = sportClassSlice.actions;

export const selectSportClasses = (state: RootState) =>
  state[name].sportClasses;
