import { CreateLibraryType } from "../../../models";

export function IsTypeNameInvalid(createLibraryType: CreateLibraryType): boolean {
  return createLibraryType.name === "";
}

export default IsTypeNameInvalid;