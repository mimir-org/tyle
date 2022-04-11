import { CreateLibraryType } from "../../../models";
import { IsLocation } from "../helpers";

export function IsObjectSelectionInvalid(createLibraryType: CreateLibraryType): boolean {
  return createLibraryType.objectType === 0 && !IsLocation(createLibraryType.aspect);
}

export default IsObjectSelectionInvalid;