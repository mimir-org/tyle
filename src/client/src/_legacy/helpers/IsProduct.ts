import { Aspect, CreateLibraryType, LibItem, Node } from "../models";

const IsProduct = (item: Node | LibItem | CreateLibraryType) => {
  return item?.aspect === Aspect.Product;
};

export default IsProduct;
