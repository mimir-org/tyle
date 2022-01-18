import { CreateLibraryType } from "../../../models";
import { IsLocation } from "../helpers";

export function IsPredefinedAttributesSelectionInvalid(createLibraryType: CreateLibraryType): boolean {
  return IsLocation(createLibraryType.aspect) && createLibraryType.predefinedAttributes.length === 0;
}

export default IsPredefinedAttributesSelectionInvalid;