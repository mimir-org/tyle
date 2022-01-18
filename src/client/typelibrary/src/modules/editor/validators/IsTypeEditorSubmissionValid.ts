import { CreateLibraryType } from "../../../models";
import {
  IsAspectSelectionInvalid,
  IsAttributeTypesSelectionInvalid,
  IsLocationSelectionInvalid,
  IsObjectSelectionInvalid,
  IsPredefinedAttributesSelectionInvalid,
  IsPurposeSelectionInvalid,
  IsRdsSelectionInvalid,
  IsSymbolSelectionInvalid,
  IsTerminalTypesSelectionInvalid,
  IsTypeNameInvalid,
} from "./index";

export function IsTypeEditorSubmissionValid(createLibraryType: CreateLibraryType): boolean {
  return [
    IsAspectSelectionInvalid,
    IsObjectSelectionInvalid,
    IsPurposeSelectionInvalid,
    IsLocationSelectionInvalid,
    IsSymbolSelectionInvalid,
    IsTypeNameInvalid,
    IsRdsSelectionInvalid,
    IsTerminalTypesSelectionInvalid,
    IsAttributeTypesSelectionInvalid,
    IsPredefinedAttributesSelectionInvalid,
  ].every(func => !func(createLibraryType))
}

export default IsTypeEditorSubmissionValid;