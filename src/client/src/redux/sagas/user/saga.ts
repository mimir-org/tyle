import { call, put } from "redux-saga/effects";
import { User } from "../../../models";
import Config from "../../../models/Config";
import { ApiError, get } from "../../../models/webclient";
import { fetchUserSuccessOrError } from "../../store/user/userSlice";

export function* getUser() {
  try {
    const url = Config.API_BASE_URL + "user";
    const response = yield call(get, url);

    const user: User = {
      name: response?.data?.name,
      email: response?.data?.email,
      role: response?.data?.role,
    };

    yield put(fetchUserSuccessOrError({ user: user, apiError: null }));
  } catch (error) {
    const apiError: ApiError = {
      key: fetchUserSuccessOrError.type,
      errorMessage: error?.message,
      errorData: null,
    };

    yield put(fetchUserSuccessOrError({ user: null, apiError: apiError }));
  }
}
