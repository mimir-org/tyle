import { call, put } from "redux-saga/effects";
import { saveAs } from "file-saver";
import {
  GetApiErrorForBadRequest,
  GetApiErrorForException,
  get,
  post,
} from "../../../models/webclient";
import { PayloadAction } from "@reduxjs/toolkit";
import { CreateLibraryType } from "../../../models";
import {
  exportLibrarySuccessOrError,
  fetchLibraryInterfaceTypesSuccessOrError,
  fetchLibrarySuccessOrError,
  fetchLibraryTransportTypesSuccessOrError,
  importLibrarySuccessOrError,
} from "../../store/library/librarySlice";
import Config from "../../../models/Config";

export function* searchLibrary(action: PayloadAction<string>) {
  const emptyPayload = {
    nodeTypes: [],
    transportTypes: [],
    interfaceTypes: [],
    subProjectTypes: [],
  };

  try {
    const url = `${Config.API_BASE_URL}librarytype?name=${action.payload}`;
    const response = yield call(get, url);

    if (response.status === 400) {
      const apiError = GetApiErrorForBadRequest(
        response,
        fetchLibrarySuccessOrError.type
      );
      yield put(fetchLibrarySuccessOrError({ ...emptyPayload, apiError }));
      return;
    }

    const payload = {
      nodeTypes: response.data.objectBlocks,
      transportTypes: response.data.transports,
      interfaceTypes: response.data.interfaces,
      subProjectTypes: response.data.subProjects,
    };

    yield put(fetchLibrarySuccessOrError({ ...payload, apiError: null }));
  } catch (error) {
    const apiError = GetApiErrorForException(
      error,
      fetchLibrarySuccessOrError.type
    );
    yield put(fetchLibrarySuccessOrError({ ...emptyPayload, apiError }));
  }
}

export function* exportLibrary(action: PayloadAction<string>) {
  try {
    const url = `${Config.API_BASE_URL}librarytypefile/export`;
    const response = yield call(get, url);

    if (response.status === 400) {
      const apiError = GetApiErrorForBadRequest(
        response,
        exportLibrarySuccessOrError.type
      );
      yield put(exportLibrarySuccessOrError(apiError));
      return;
    }

    const blob = new Blob([JSON.stringify(response.data, null, 2)], {
      type: "application/json",
    });
    saveAs(blob, action.payload + ".json");

    yield put(exportLibrarySuccessOrError(null));
  } catch (error) {
    const apiError = GetApiErrorForException(
      error,
      exportLibrarySuccessOrError.type
    );
    yield put(exportLibrarySuccessOrError(apiError));
  }
}

export function* importLibrary(action: PayloadAction<CreateLibraryType[]>) {
  try {
    const url = `${Config.API_BASE_URL}librarytypefile/import`;
    const response = yield call(post, url, action.payload);

    if (response.status === 400) {
      const apiError = GetApiErrorForBadRequest(
        response,
        importLibrarySuccessOrError.type
      );
      yield put(importLibrarySuccessOrError(apiError));
      return;
    }

    yield put(importLibrarySuccessOrError(null));
  } catch (error) {
    const apiError = GetApiErrorForException(
      error,
      importLibrarySuccessOrError.type
    );
    yield put(importLibrarySuccessOrError(apiError));
  }
}

export function* getTransportTypes() {
  try {
    const url = `${Config.API_BASE_URL}librarytype/transport`;
    const response = yield call(get, url);

    if (response.status === 400) {
      const apiError = GetApiErrorForBadRequest(
        response,
        fetchLibraryTransportTypesSuccessOrError.type
      );
      yield put(
        fetchLibraryTransportTypesSuccessOrError({ libraryItems: [], apiError })
      );
      return;
    }

    yield put(
      fetchLibraryTransportTypesSuccessOrError({
        libraryItems: response.data,
        apiError: null,
      })
    );
  } catch (error) {
    const apiError = GetApiErrorForException(
      error,
      fetchLibraryTransportTypesSuccessOrError.type
    );
    yield put(
      fetchLibraryTransportTypesSuccessOrError({ libraryItems: [], apiError })
    );
  }
}

export function* getInterfaceTypes() {
  try {
    const url = `${Config.API_BASE_URL}librarytype/interface`;
    const response = yield call(get, url);

    if (response.status === 400) {
      const apiError = GetApiErrorForBadRequest(
        response,
        fetchLibraryInterfaceTypesSuccessOrError.type
      );
      yield put(
        fetchLibraryInterfaceTypesSuccessOrError({ libraryItems: [], apiError })
      );
      return;
    }

    yield put(
      fetchLibraryInterfaceTypesSuccessOrError({
        libraryItems: response.data,
        apiError: null,
      })
    );
  } catch (error) {
    const apiError = GetApiErrorForException(
      error,
      fetchLibraryInterfaceTypesSuccessOrError.type
    );
    yield put(
      fetchLibraryInterfaceTypesSuccessOrError({ libraryItems: [], apiError })
    );
  }
}
