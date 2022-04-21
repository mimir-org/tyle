import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import { ApiError } from "../../../models/webclient";
import { defaultCreateLibraryType, fromJsonCreateLibraryType } from "../../../models/application/classes/CreateLibraryType";
import {
  FetchingBlobDataActionFinished,
  FetchingSimpleTypesActionFinished,
  FetchingTypeAction,
  TypeEditorState,
  UpdateCreateLibraryType,
} from "./types";
import {
  AttributeType,
  BlobData,
  CreateLibraryType,
  LocationType,
  PredefinedAttribute,
  Purpose,
  Rds,
  TerminalTypeDictItem,
  TerminalTypeItem,
} from "../../../models";

const initialTypeEditorState: TypeEditorState = {
  visible: false,
  fetching: false,
  creating: false,
  validationVisibility: false,
  createLibraryType: { ...defaultCreateLibraryType },
  purposes: [],
  rdsList: [],
  terminals: [],
  attributes: [],
  locationTypes: [],
  predefinedAttributes: [],
  simpleTypes: [],
  apiError: [],
  icons: [] as BlobData[],
};

export const editorSlice = createSlice({
  name: 'editor',
  initialState: initialTypeEditorState,
  reducers: {
    fetchInitialData: (state) => {
      state.fetching = true;
    },
    fetchInitialDataSuccessOrError: (state, action: PayloadAction<Purpose[]>) => {
      state.fetching = false;
      state.purposes = action.payload;
    },
    fetchRdsSuccessOrError: (state, action: PayloadAction<Rds[]>) => {
      state.fetching = false;
      state.rdsList = action.payload ? action.payload : [];
    },
    fetchTerminalsSuccessOrError: (state, action: PayloadAction<TerminalTypeDictItem[]>) => {
      state.fetching = false;
      state.terminals = action.payload;
    },
    fetchAttributesSuccessOrError: (state, action: PayloadAction<AttributeType[]>) => {
      state.fetching = false;
      state.attributes = action.payload ? action.payload : [];
    },
    fetchLocationTypesSuccessOrError: (state, action: PayloadAction<LocationType[]>) => {
      state.fetching = false;
      state.locationTypes = action.payload;
    },
    fetchPredefinedAttributesSuccessOrError: (state, action: PayloadAction<PredefinedAttribute[]>) => {
      state.fetching = false;
      state.predefinedAttributes = action.payload;
    },
    fetchCreateLibraryType: (state, _action: PayloadAction<FetchingTypeAction>) => {
      state.fetching = true;
      state.visible = false;
      state.createLibraryType = { ...initialTypeEditorState.createLibraryType };
    },
    fetchCreateLibraryTypeSuccessOrError: (state, action: PayloadAction<CreateLibraryType>) => {
      state.fetching = false;
      state.visible = true;
      state.createLibraryType = fromJsonCreateLibraryType(action.payload);
    },
    fetchBlobData: (state) => {
      state.fetching = true;
      state.apiError = state.apiError
        ? state.apiError.filter((elem) => elem.key !== fetchBlobDataSuccessOrError.type)
        : state.apiError;
    },
    fetchBlobDataSuccessOrError: (state, action: PayloadAction<FetchingBlobDataActionFinished>) => {
      state.fetching = false;
      state.icons = action.payload.icons;
      action.payload.apiError && state.apiError.push(action.payload.apiError);
    },
    fetchSimpleTypes: (state) => {
      state.fetching = true;
      state.apiError = state.apiError
        ? state.apiError.filter((elem) => elem.key !== fetchSimpleTypesSuccessOrError.type)
        : state.apiError;
    },
    fetchSimpleTypesSuccessOrError: (state, action: PayloadAction<FetchingSimpleTypesActionFinished>) => {
      state.fetching = false;
      state.simpleTypes = action.payload.simpleTypes;
      action.payload.apiError && state.apiError.push(action.payload.apiError);
    },
    updateCreateLibraryType: (state, action: PayloadAction<UpdateCreateLibraryType>) => {
      state.createLibraryType = {
        ...state.createLibraryType,
        [action.payload.key]: action.payload.value,
      };
    },
    addTerminalType: (state, action: PayloadAction<TerminalTypeItem>) => {
      state.createLibraryType = {
        ...state.createLibraryType,
        terminalTypes: [...state.createLibraryType.terminalTypes, action.payload],
      };
    },
    removeTerminalType: (state, action: PayloadAction<TerminalTypeItem>) => {
      state.createLibraryType = {
        ...state.createLibraryType,
        terminalTypes: state.createLibraryType.terminalTypes.filter(
          (terminal) => terminal.terminalId !== action.payload.terminalId
        ),
      };
    },
    updateTerminalType: (state, action: PayloadAction<TerminalTypeItem>) => {
      state.createLibraryType = {
        ...state.createLibraryType,
        terminalTypes: state.createLibraryType.terminalTypes.map((terminal) =>
          terminal.terminalId === action.payload.terminalId ? action.payload : terminal
        ),
      };
    },
    removeTerminalTypeByCategory: (state, action: PayloadAction<string>) => {
      state.createLibraryType = {
        ...state.createLibraryType,
        terminalTypes: state.createLibraryType.terminalTypes.filter((terminal) => terminal.categoryId !== action.payload),
      };
    },
    clearAllTerminalTypes: (state) => {
      state.createLibraryType.terminalTypes = [];
    },
    saveLibraryType: (state, _action: PayloadAction<CreateLibraryType>) => {
      state.fetching = true;
      state.apiError = state.apiError ? state.apiError.filter((elem) => elem.key !== saveLibraryType.type) : state.apiError;
    },
    saveLibraryTypeSuccessOrError: (state, action: PayloadAction<ApiError | null>) => {
      state.fetching = false;
      action.payload && state.apiError.push(action.payload);
      state.createLibraryType = { ...initialTypeEditorState.createLibraryType };
    },
    deleteTypeEditorError: (state, action: PayloadAction<string>) => {
      state.apiError = state.apiError ? state.apiError.filter((elem) => elem.key !== action.payload) : state.apiError;
    },
    changeTypeEditorVisibility: (state, action: PayloadAction<boolean>) => {
      state.fetching = false;
      state.visible = action.payload;
      state.createLibraryType = { ...initialTypeEditorState.createLibraryType };
    },
    changeTypeEditorValidationVisibility: (state, action: PayloadAction<boolean>) => {
      state.validationVisibility = action.payload;
    },
  }
})

export const {
  fetchInitialData,
  fetchInitialDataSuccessOrError,
  fetchRdsSuccessOrError,
  fetchTerminalsSuccessOrError,
  fetchAttributesSuccessOrError,
  fetchLocationTypesSuccessOrError,
  fetchPredefinedAttributesSuccessOrError,
  fetchCreateLibraryType,
  fetchCreateLibraryTypeSuccessOrError,
  fetchBlobData,
  fetchBlobDataSuccessOrError,
  fetchSimpleTypes,
  fetchSimpleTypesSuccessOrError,
  updateCreateLibraryType,
  addTerminalType,
  removeTerminalType,
  updateTerminalType,
  removeTerminalTypeByCategory,
  clearAllTerminalTypes,
  saveLibraryType,
  saveLibraryTypeSuccessOrError,
  deleteTypeEditorError,
  changeTypeEditorVisibility,
  changeTypeEditorValidationVisibility,
} = editorSlice.actions;

export default editorSlice.reducer