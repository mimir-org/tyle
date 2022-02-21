import { ListType } from "../TypeEditorList";
import { CreateLibraryType } from "../../../models";
import { IsFunction, IsObjectBlock, IsProduct } from "./index";

const ShowObjectBlock = (listType: ListType, createLibraryType: CreateLibraryType): boolean => {
  return (
    listType === ListType.Terminals &&
    ((IsFunction(createLibraryType?.aspect) && IsObjectBlock(createLibraryType?.objectType)) ||
      (IsProduct(createLibraryType?.aspect) && IsObjectBlock(createLibraryType?.objectType)))
  );
};
export default ShowObjectBlock;
