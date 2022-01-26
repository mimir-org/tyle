import { ApiError } from "../../../models/webclient";
import {
  AttributeType,
  BlobData,
  CreateLibraryType,
  LibraryFilter,
  LocationType,
  PredefinedAttribute,
  Purpose,
  Rds,
  SimpleType,
  TerminalTypeDict,
} from "../../../models";

// State types
export interface TypeEditorState {
  visible: boolean;
  fetching: boolean;
  creating: boolean;
  validationVisibility: boolean;
  createLibraryType: CreateLibraryType;
  purposes: Purpose[];
  rdsList: Rds[];
  terminals: TerminalTypeDict;
  attributes: AttributeType[];
  locationTypes: LocationType[];
  predefinedAttributes: PredefinedAttribute[];
  simpleTypes: SimpleType[];
  apiError: ApiError[];
  icons: BlobData[];
}

// Action types
export interface FetchingTypeAction {
  selectedType: string;
  filter: LibraryFilter;
}

export interface FetchingBlobDataActionFinished {
  icons: BlobData[];
  apiError: ApiError;
}

export interface FetchingSimpleTypesActionFinished {
  simpleTypes: SimpleType[];
  apiError?: ApiError;
}

export interface UpdateCreateLibraryType {
  key: string;
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  value: any;
}
