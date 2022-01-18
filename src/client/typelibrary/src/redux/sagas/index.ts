import { all, spawn, takeEvery } from "redux-saga/effects";
import { exportLibrary, getInterfaceTypes, getTransportTypes, importLibrary, searchLibrary } from "./library/saga";
import { getUser } from "./user/saga";
import { fetchUser } from "../store/user/userSlice";
import {
  exportLibrary as exportLibraryAction,
  fetchLibrary,
  fetchLibraryInterfaceTypes,
  fetchLibraryTransportTypes,
  importLibrary as importLibraryAction
} from "../store/library/librarySlice";
import {
  getPredefinedAttributes,
  getLocationTypes,
  getInitialData,
  getRDS,
  getAttributes,
  getBlobData,
  getSelectedCreateLibraryType,
  getSimpleTypes,
  getTerminals,
  saveType,
} from "./editor/saga";
import {
  fetchBlobData,
  fetchCreateLibraryType,
  fetchInitialData,
  fetchSimpleTypes,
  saveLibraryType
} from "../store/editor/editorSlice";

function* sagas() {
  yield all([
    takeEvery(fetchUser, getUser),
    takeEvery(fetchLibrary, searchLibrary),
    takeEvery(fetchLibraryTransportTypes, getTransportTypes),
    takeEvery(fetchLibraryInterfaceTypes, getInterfaceTypes),
    takeEvery(exportLibraryAction, exportLibrary),
    takeEvery(importLibraryAction, importLibrary),
    takeEvery(fetchInitialData, getInitialData),
    takeEvery(fetchInitialData, getLocationTypes),
    takeEvery(fetchInitialData, getRDS),
    takeEvery(fetchInitialData, getTerminals),
    takeEvery(fetchInitialData, getPredefinedAttributes),
    takeEvery(fetchInitialData, getAttributes),
    takeEvery(fetchSimpleTypes, getSimpleTypes),
    takeEvery(fetchBlobData, getBlobData),
    takeEvery(fetchCreateLibraryType, getSelectedCreateLibraryType),
    takeEvery(saveLibraryType, saveType),
  ]);
}

export function* rootSaga() {
  yield spawn(sagas);
}
