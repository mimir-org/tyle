import createSagaMiddleware from "redux-saga";
import editorReducer from "./editor/editorSlice";
import userReducer from "./user/userSlice";
import validationReducer from "./validation/validationSlice";
import libraryReducer from "./library/librarySlice";
import { configureStore } from "@reduxjs/toolkit";
import { combineReducers } from "redux";
import { rootSaga } from "../sagas";

const rootReducers = combineReducers({
  library: libraryReducer,
  editor: editorReducer,
  userState: userReducer,
  validation: validationReducer,
});

const sagaMiddleware = createSagaMiddleware();

const store = configureStore({
  reducer: rootReducers,
  devTools: process.env.NODE_ENV !== "production" && window["__REDUX_DEVTOOLS_EXTENSION_COMPOSE__"],
  middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(sagaMiddleware),
});

export type RootState = ReturnType<typeof rootReducers>;
export type AppDispatch = typeof store.dispatch;
export * from "./hooks";
export * from "./selectors";

// eslint-disable-next-line import/no-anonymous-default-export
export default { store };

sagaMiddleware.run(rootSaga);
