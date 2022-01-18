import { CreateLibraryType } from "../../../models";

export function IsAttributeTypesSelectionInvalid(createLibraryType: CreateLibraryType): boolean {
  return createLibraryType.attributeTypes.length === 0;
}

export default IsAttributeTypesSelectionInvalid;