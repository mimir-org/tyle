import { FetchUserActionFinished, UserState } from "./types";
import { PayloadAction, createSlice } from "@reduxjs/toolkit";

const initialUserState: UserState = {
  fetching: false,
  user: null,
  apiError: [],
};

export const userSlice = createSlice({
  name: 'user',
  initialState: initialUserState,
  reducers: {
    fetchUser: (state) => {
      state.fetching = true;
      state.user = null;
      state.apiError = state.apiError ? state.apiError.filter((elem) => elem.key !== fetchUserSuccessOrError.type) : state.apiError
    },
    fetchUserSuccessOrError: (state, action: PayloadAction<FetchUserActionFinished>) => {
      state.fetching = false;
      state.user = action.payload.user;
      state.apiError = action.payload.apiError ? [...state.apiError, action.payload.apiError] : state.apiError;
    },
    deleteUserError: (state, action: PayloadAction<string>) => {
      state.apiError = state.apiError ? state.apiError.filter((elem) => elem.key !== action.payload) : state.apiError;
    }
  }
})

export const { fetchUser, fetchUserSuccessOrError, deleteUserError } = userSlice.actions;
export default userSlice.reducer;