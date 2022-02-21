import { CreateLibraryType, LibItem, ObjectType } from "../../../models";
import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import { FetchLibrary, FetchLibraryItems, LibraryState } from "./types";
import { ApiError } from "../../../models/webclient";

const initialLibraryState: LibraryState = {
  fetching: false,
  nodeTypes: [],
  apiError: [],
  transportTypes: [],
  interfaceTypes: [],
  subProjectTypes: [],
};

export const librarySlice = createSlice({
  name: "library",
  initialState: initialLibraryState,
  reducers: {
    fetchLibrary: (state, _action: PayloadAction<string>) => {
      state.fetching = true;
      state.nodeTypes = [];
      state.transportTypes = [];
      state.interfaceTypes = [];
      state.apiError = state.apiError
        ? state.apiError.filter((elem) => elem.key !== fetchLibrarySuccessOrError.type)
        : state.apiError;
    },
    fetchLibrarySuccessOrError: (state, action: PayloadAction<FetchLibrary>) => {
      state.fetching = false;
      const { nodeTypes = [], transportTypes = [], interfaceTypes = [], subProjectTypes = [] } = action.payload;
      Object.assign(state, { nodeTypes, transportTypes, interfaceTypes, subProjectTypes });
      action.payload.apiError && state.apiError.push(action.payload.apiError);
    },
    exportLibrary: (state, _action: PayloadAction<string>) => {
      state.fetching = true;
      state.apiError = state.apiError
        ? state.apiError.filter((elem) => elem.key !== exportLibrarySuccessOrError.type)
        : state.apiError;
    },
    exportLibrarySuccessOrError: (state, action: PayloadAction<ApiError>) => {
      state.fetching = false;
      action.payload && state.apiError.push(action.payload);
    },
    importLibrary: (state, _action: PayloadAction<CreateLibraryType[]>) => {
      state.fetching = true;
      state.apiError = state.apiError
        ? state.apiError.filter((elem) => elem.key !== importLibrarySuccessOrError.type)
        : state.apiError;
    },
    importLibrarySuccessOrError: (state, action: PayloadAction<ApiError>) => {
      state.fetching = false;
      action.payload && state.apiError.push(action.payload);
    },
    fetchLibraryTransportTypes: (state) => {
      state.fetching = true;
      state.apiError = state.apiError
        ? state.apiError.filter((elem) => elem.key !== fetchLibraryTransportTypesSuccessOrError.type)
        : state.apiError;
    },
    fetchLibraryTransportTypesSuccessOrError: (state, action: PayloadAction<FetchLibraryItems>) => {
      state.fetching = false;
      state.transportTypes = action.payload.libraryItems;
      action.payload.apiError && state.apiError.push(action.payload.apiError);
    },
    fetchLibraryInterfaceTypes: (state) => {
      state.fetching = true;
      state.apiError = state.apiError
        ? state.apiError.filter((elem) => elem.key !== fetchLibraryInterfaceTypesSuccessOrError.type)
        : state.apiError;
    },
    fetchLibraryInterfaceTypesSuccessOrError: (state, action: PayloadAction<FetchLibraryItems>) => {
      state.fetching = false;
      state.transportTypes = action.payload.libraryItems;
      action.payload.apiError && state.apiError.push(action.payload.apiError);
    },
    addLibraryItem: (state, action: PayloadAction<LibItem>) => {
      action.payload.libraryType === ObjectType.Interface && state.interfaceTypes.push(action.payload);
      action.payload.libraryType === ObjectType.ObjectBlock && state.nodeTypes.push(action.payload);
      action.payload.libraryType === ObjectType.Transport && state.transportTypes.push(action.payload);
    },
    removeLibraryItem: (state, action: PayloadAction<string>) => {
      state.interfaceTypes = state.interfaceTypes.filter((x) => x.id !== action.payload);
      state.nodeTypes = state.nodeTypes.filter((x) => x.id !== action.payload);
      state.transportTypes = state.transportTypes.filter((x) => x.id !== action.payload);
    },
    deleteLibraryError: (state, action: PayloadAction<string>) => {
      state.apiError = state.apiError ? state.apiError.filter((elem) => elem.key !== action.payload) : state.apiError;
    },
  },
});

export const {
  fetchLibrary,
  fetchLibrarySuccessOrError,
  exportLibrary,
  exportLibrarySuccessOrError,
  importLibrary,
  importLibrarySuccessOrError,
  fetchLibraryTransportTypes,
  fetchLibraryTransportTypesSuccessOrError,
  fetchLibraryInterfaceTypes,
  fetchLibraryInterfaceTypesSuccessOrError,
  addLibraryItem,
  removeLibraryItem,
  deleteLibraryError,
} = librarySlice.actions;

export default librarySlice.reducer;
