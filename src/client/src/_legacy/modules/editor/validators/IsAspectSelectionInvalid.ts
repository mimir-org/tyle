import { CreateLibraryType } from "../../../models";

export function IsAspectSelectionInvalid(createLibraryType: CreateLibraryType): boolean {
  return createLibraryType.aspect === 0;
}

export default IsAspectSelectionInvalid;