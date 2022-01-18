import { CreateLibraryType } from "../../../models";
import { IsLocation } from "../helpers";

export function IsTerminalTypesSelectionInvalid(createLibraryType: CreateLibraryType): boolean {
  return !IsLocation(createLibraryType.aspect) && (createLibraryType.terminalTypes.length === 0 && !(createLibraryType.terminalTypeId));
}

export default IsTerminalTypesSelectionInvalid;