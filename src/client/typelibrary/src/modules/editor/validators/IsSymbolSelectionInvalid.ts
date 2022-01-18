import { CreateLibraryType } from "../../../models";

export function IsSymbolSelectionInvalid(createLibraryType: CreateLibraryType): boolean {
  return createLibraryType.symbolId === "";
}

export default IsSymbolSelectionInvalid;