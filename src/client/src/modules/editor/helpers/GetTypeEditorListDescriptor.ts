import { ListType, TypeEditorListProps } from "../TypeEditorList";
import { TypeEditorState } from "../../../redux/store/editor/types";
import { Dispatch } from "redux";
import { TextResources } from "../../../assets/text";
import { OnPropertyChange, OnTerminalCategoryChange } from "../handlers";
import { IsLocation, IsProduct } from "./index";
import {
  IsAttributeTypesSelectionInvalid,
  IsPredefinedAttributesSelectionInvalid,
  IsRdsSelectionInvalid,
  IsTerminalTypesSelectionInvalid,
} from "../validators";

interface TypeEditorListDescriptor extends TypeEditorListProps {
  isVisible: boolean;
  validation: {
    visible: boolean;
    message: string;
  };
}

/**
 * Generates a descriptor for a given ListType.
 * The descriptor describes the view state of a ListType given the application state provided
 * @param listType ListType to generate descriptor for
 * @param state State of the TypeEditor at render
 * @param dispatch General dispatch function for the application
 */
export function GetTypeEditorListDescriptor(
  listType: ListType,
  state: TypeEditorState,
  dispatch: Dispatch
): TypeEditorListDescriptor | null {
  switch (listType) {
    case ListType.Rds:
      return {
        listType: ListType.Rds,
        isVisible: true,
        items: state?.rdsList,
        createLibraryType: state?.createLibraryType,
        onPropertyChange: (key, data) => OnPropertyChange(key, data, dispatch),
        validation: {
          visible: state?.validationVisibility && IsRdsSelectionInvalid(state?.createLibraryType),
          message: TextResources.TypeEditor_Error_RDS,
        },
      };
    case ListType.Terminals:
      return {
        listType: ListType.Terminals,
        isVisible: !IsLocation(state?.createLibraryType.aspect),
        items: state?.terminals,
        createLibraryType: state?.createLibraryType,
        onPropertyChange: (key, data) => OnPropertyChange(key, data, dispatch),
        onTerminalCategoryChange: (key, data) => OnTerminalCategoryChange(key, data, dispatch),
        validation: {
          visible: state?.validationVisibility && IsTerminalTypesSelectionInvalid(state?.createLibraryType),
          message: TextResources.TypeEditor_Error_Terminals,
        },
      };
    case ListType.PredefinedAttributes:
      return {
        listType: ListType.PredefinedAttributes,
        isVisible: IsLocation(state?.createLibraryType.aspect),
        items: state?.predefinedAttributes,
        createLibraryType: state?.createLibraryType,
        onPropertyChange: (key, data) => OnPropertyChange(key, data, dispatch),
        validation: {
          visible: state?.validationVisibility && IsPredefinedAttributesSelectionInvalid(state?.createLibraryType),
          message: TextResources.TypeEditor_Error_Location_Attributes,
        },
      };
    case ListType.ObjectAttributes:
      return {
        listType: ListType.ObjectAttributes,
        isVisible: !IsLocation(state?.createLibraryType.aspect),
        items: state?.attributes,
        createLibraryType: state?.createLibraryType,
        onPropertyChange: (key, data) => OnPropertyChange(key, data, dispatch),
        validation: {
          visible: state?.validationVisibility && IsAttributeTypesSelectionInvalid(state?.createLibraryType),
          message: TextResources.TypeEditor_Error_Attributes,
        },
      };
    case ListType.LocationAttributes:
      return {
        listType: ListType.LocationAttributes,
        isVisible: IsLocation(state?.createLibraryType.aspect),
        items: state?.attributes,
        createLibraryType: state?.createLibraryType,
        onPropertyChange: (key, data) => OnPropertyChange(key, data, dispatch),
        validation: {
          visible: state?.validationVisibility && IsAttributeTypesSelectionInvalid(state?.createLibraryType),
          message: TextResources.TypeEditor_Error_Attributes,
        },
      };
    case ListType.SimpleTypes:
      return {
        listType: ListType.SimpleTypes,
        isVisible: IsProduct(state?.createLibraryType.aspect),
        items: state?.simpleTypes,
        createLibraryType: state?.createLibraryType,
        onPropertyChange: (key, data) => OnPropertyChange(key, data, dispatch),
        validation: {
          visible: false,
          message: "",
        },
      };
    default:
      return null;
  }
}
