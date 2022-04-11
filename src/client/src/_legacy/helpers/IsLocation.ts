import { Aspect, CreateLibraryType, LibItem, Node } from "../models";

const IsLocation = (item: Node | LibItem | CreateLibraryType) => {
  return item?.aspect === Aspect.Location;
};

export default IsLocation;
