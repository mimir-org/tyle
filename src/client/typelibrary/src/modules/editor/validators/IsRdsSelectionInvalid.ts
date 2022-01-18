import { CreateLibraryType } from "../../../models";

export function IsRdsSelectionInvalid(createLibraryType: CreateLibraryType): boolean {
  return createLibraryType.rdsId === "";
}

export default IsRdsSelectionInvalid;