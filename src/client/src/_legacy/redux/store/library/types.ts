import { LibItem, LibrarySubProjectItem } from "../../../models";
import { ApiError } from "../../../models/webclient";

// State types
export interface LibraryState {
  fetching: boolean;
  nodeTypes: LibItem[] | null;
  apiError: ApiError[];
  transportTypes: LibItem[];
  interfaceTypes: LibItem[];
  subProjectTypes: LibrarySubProjectItem[];
}

// Action types
export interface FetchLibrary {
  nodeTypes: LibItem[];
  transportTypes: LibItem[];
  interfaceTypes: LibItem[];
  subProjectTypes: LibrarySubProjectItem[];
  apiError: ApiError;
}

export interface FetchLibraryItems {
  libraryItems: LibItem[];
  apiError: ApiError;
}