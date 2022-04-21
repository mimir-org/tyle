import { IsFunction, IsInterface, IsProduct, IsTransport } from "../../../helpers";
import { CreateLibraryType } from "../../../../../models";

const IsTransportOrInterface = (createLibraryType: CreateLibraryType) => {
  if (IsFunction(createLibraryType?.aspect) || IsProduct(createLibraryType?.aspect)) {
    return IsTransport(createLibraryType?.objectType) || IsInterface(createLibraryType?.objectType);
  }
  return false;
};

export default IsTransportOrInterface;
