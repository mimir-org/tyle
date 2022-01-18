import { ListType } from "../TypeEditorList";
import { TextResources } from "../../../assets/text";
import { CreateLibraryType } from "../../../models";
import { IsInterface, IsLocation, IsTransport } from "./index";

const GetListLabel = (listType: ListType, createLibraryType: CreateLibraryType): string => {
  if (listType === ListType.Rds) return TextResources.TypeEditor_Properties_RDS;

  if (
    listType === ListType.Terminals &&
    !IsLocation(createLibraryType.aspect) &&
    !(IsInterface(createLibraryType.objectType) || IsTransport(createLibraryType.objectType))
  )
    return TextResources.TypeEditor_Properties_Terminals;

  if (listType === ListType.Terminals && (IsInterface(createLibraryType.objectType) || IsTransport(createLibraryType.objectType)))
    return TextResources.TypeEditor_Properties_Terminal_Type;

  if ((listType === ListType.Terminals || listType === ListType.PredefinedAttributes) && IsLocation(createLibraryType.aspect))
    return TextResources.TypeEditor_Properties_Predefined_Location_Attributes;

  if (listType === ListType.ObjectAttributes) return TextResources.TypeEditor_Properties_Block_Attributes;
  if (listType === ListType.LocationAttributes) return TextResources.TypeEditor_Properties_Location_Attributes;
  if (listType === ListType.SimpleTypes) return TextResources.TypeEditor_Properties_Simple_Types;

  return "";
};

export default GetListLabel;
