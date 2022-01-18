import { CreateLibraryType } from "../../../models";
import { IsLocation } from "../helpers";

export function IsLocationSelectionInvalid(createLibraryType: CreateLibraryType): boolean {
  return createLibraryType.locationType === "" && IsLocation(createLibraryType.aspect);
}

export default IsLocationSelectionInvalid;