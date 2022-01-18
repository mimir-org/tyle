import { Aspect, CreateLibraryType, LibItem, Node } from "../models";

const IsFunction = (item: Node | LibItem | CreateLibraryType) => {
  return item?.aspect === Aspect.Function;
};

export default IsFunction;
