import { Validation } from "./types";
import { PayloadAction, createSlice } from "@reduxjs/toolkit";

const initialValidationState: Validation = {
  valid: true,
  message: "",
};

export const validationSlice = createSlice({
  name: 'validation',
  initialState: initialValidationState,
  reducers: {
    setValidation: (state, action: PayloadAction<Validation>) => {
      state.valid = action.payload.valid;
      state.message = action.payload.message;
    }
  }
})

export const { setValidation } = validationSlice.actions;
export default validationSlice.reducer;