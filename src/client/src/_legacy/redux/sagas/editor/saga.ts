import { call, put as statePut } from "redux-saga/effects";
import { PayloadAction } from "@reduxjs/toolkit";
import {
  addLibraryItem,
  removeLibraryItem,
} from "../../store/library/librarySlice";
import {
  get,
  post,
  GetBadResponseData,
  ApiError,
  HttpResponse,
} from "../../../models/webclient";
import { FetchingTypeAction } from "../../store/editor/types";
import {
  Aspect,
  AttributeType,
  BlobData,
  CreateLibraryType,
  LibItem,
  LocationType,
  PredefinedAttribute,
  Purpose,
  Rds,
  SimpleType,
  SimpleTypeResponse,
  TerminalTypeDictItem,
} from "../../../models";
import {
  fetchAttributesSuccessOrError,
  fetchBlobDataSuccessOrError,
  fetchCreateLibraryTypeSuccessOrError,
  fetchInitialDataSuccessOrError,
  fetchLocationTypesSuccessOrError,
  fetchPredefinedAttributesSuccessOrError,
  fetchRdsSuccessOrError,
  fetchSimpleTypesSuccessOrError,
  fetchTerminalsSuccessOrError,
  saveLibraryTypeSuccessOrError,
} from "../../store/editor/editorSlice";
import Config from "../../../../models/Config";

export function* saveType(action: PayloadAction<CreateLibraryType>) {
  try {
    const createLibraryType = action.payload;

    const url = createLibraryType.id
      ? `${Config.API_BASE_URL}librarytype/${createLibraryType.id}`
      : `${Config.API_BASE_URL}librarytype`;

    const response: HttpResponse<LibItem> = yield call(
      post,
      url,
      createLibraryType
    );

    if (response.status === 400) {
      const apiError = getApiErrorForBadRequest(
        saveLibraryTypeSuccessOrError.type,
        response
      );
      yield statePut(saveLibraryTypeSuccessOrError(apiError));
      return;
    }

    yield statePut(saveLibraryTypeSuccessOrError(null));

    // LIBRARY: Remove item
    yield statePut(removeLibraryItem(createLibraryType.id));

    // LIBRARY: Add the created library item back in
    yield statePut(addLibraryItem(response.data));
  } catch (error) {
    const apiError = getApiErrorForException(
      saveLibraryTypeSuccessOrError.type,
      error
    );
    yield statePut(saveLibraryTypeSuccessOrError(apiError));
  }
}

export function* getInitialData() {
  try {
    const endpointUrl = `${Config.API_BASE_URL}enum/purpose`;
    const purposesResponse: HttpResponse<Purpose[]> = yield call(
      get,
      endpointUrl
    );

    yield statePut(fetchInitialDataSuccessOrError(purposesResponse.data));
  } catch (error) {
    yield statePut(fetchInitialDataSuccessOrError([]));
  }
}

export function* getRDS(action: PayloadAction<Aspect | undefined>) {
  try {
    const aspect = action.payload ?? Aspect.NotSet;
    const endpointUrl = `${Config.API_BASE_URL}rds/${aspect}`;
    const rdsResponse: HttpResponse<Rds[]> = yield call(get, endpointUrl);

    yield statePut(fetchRdsSuccessOrError(rdsResponse.data));
  } catch (error) {
    yield statePut(fetchRdsSuccessOrError([]));
  }
}

export function* getTerminals() {
  try {
    const endpointUrl = `${Config.API_BASE_URL}terminaltype/category`;
    const terminalResponse: HttpResponse<TerminalTypeDictItem[]> = yield call(
      get,
      endpointUrl
    );

    yield statePut(fetchTerminalsSuccessOrError(terminalResponse.data));
  } catch (error) {
    yield statePut(fetchTerminalsSuccessOrError([]));
  }
}

export function* getAttributes(action: PayloadAction<Aspect | undefined>) {
  try {
    const aspect = action.payload ?? Aspect.NotSet;
    const endpointUrl = `${Config.API_BASE_URL}attributetype/${aspect}`;
    const attributesResponse: HttpResponse<AttributeType[]> = yield call(
      get,
      endpointUrl
    );

    yield statePut(fetchAttributesSuccessOrError(attributesResponse.data));
  } catch (error) {
    yield statePut(fetchAttributesSuccessOrError([]));
  }
}

export function* getLocationTypes() {
  try {
    const endpointUrl = `${Config.API_BASE_URL}enum/location`;
    const locationTypesResponse: HttpResponse<LocationType[]> = yield call(
      get,
      endpointUrl
    );

    yield statePut(
      fetchLocationTypesSuccessOrError(locationTypesResponse.data)
    );
  } catch (error) {
    yield statePut(fetchLocationTypesSuccessOrError([]));
  }
}

export function* getPredefinedAttributes() {
  try {
    const endpointUrl = `${Config.API_BASE_URL}attributetype/predefined-attributes`;
    const predefinedAttributesResponse: HttpResponse<PredefinedAttribute[]> =
      yield call(get, endpointUrl);

    yield statePut(
      fetchPredefinedAttributesSuccessOrError(predefinedAttributesResponse.data)
    );
  } catch (error) {
    yield statePut(fetchPredefinedAttributesSuccessOrError([]));
  }
}

export function* getBlobData() {
  try {
    const endpointUrl = `${Config.API_BASE_URL}blob/`;
    const response: HttpResponse<BlobData[]> = yield call(get, endpointUrl);

    // This is a bad request
    if (response.status === 400) {
      const apiError = getApiErrorForBadRequest(
        fetchBlobDataSuccessOrError.type,
        response
      );
      yield statePut(fetchBlobDataSuccessOrError({ icons: [], apiError }));
      return;
    }

    yield statePut(
      fetchBlobDataSuccessOrError({ icons: response.data, apiError: null })
    );
  } catch (error) {
    const apiError = getApiErrorForException(
      fetchBlobDataSuccessOrError.type,
      error
    );
    yield statePut(fetchBlobDataSuccessOrError({ icons: [], apiError }));
  }
}

export function* getSelectedCreateLibraryType(
  action: PayloadAction<FetchingTypeAction>
) {
  try {
    const endpointUrl = `${Config.API_BASE_URL}librarytype/${action.payload.selectedType}/${action.payload.filter}`;
    const selectedNodeResponse: HttpResponse<CreateLibraryType> = yield call(
      get,
      endpointUrl
    );
    const createLibraryType = selectedNodeResponse.data;
    createLibraryType.id = action.payload.selectedType;

    yield statePut(
      fetchCreateLibraryTypeSuccessOrError(selectedNodeResponse.data)
    );
  } catch (error) {
    yield statePut(fetchCreateLibraryTypeSuccessOrError(null));
  }
}

export function* getSimpleTypes() {
  try {
    const endpointUrl = `${Config.API_BASE_URL}librarytype/simpletype`;
    const simpleTypesURLResponse: HttpResponse<SimpleTypeResponse[]> =
      yield call(get, endpointUrl);

    const simpleTypes = simpleTypesURLResponse.data?.map((comp): SimpleType => {
      return { ...comp, attributes: comp.attributeTypes };
    });

    yield statePut(
      fetchSimpleTypesSuccessOrError({ simpleTypes, apiError: undefined })
    );
  } catch (error) {
    const apiError = getApiErrorForException(
      fetchSimpleTypesSuccessOrError.type,
      error
    );
    yield statePut(
      fetchSimpleTypesSuccessOrError({ simpleTypes: [], apiError })
    );
  }
}

// eslint-disable-next-line @typescript-eslint/no-explicit-any
function getApiErrorForBadRequest(
  key: string,
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  response: HttpResponse<any>
): ApiError {
  const data = GetBadResponseData(response);
  return {
    key,
    errorMessage: data?.title,
    errorData: data,
  };
}

function getApiErrorForException(key: string, error: Error): ApiError {
  return {
    key,
    errorMessage: error?.message,
    errorData: undefined,
  };
}
