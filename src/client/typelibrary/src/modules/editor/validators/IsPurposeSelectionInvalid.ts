import { CreateLibraryType } from "../../../models";

export function IsPurposeSelectionInvalid(createLibraryType: CreateLibraryType): boolean {
  return createLibraryType.purpose === "";
}

export default IsPurposeSelectionInvalid;