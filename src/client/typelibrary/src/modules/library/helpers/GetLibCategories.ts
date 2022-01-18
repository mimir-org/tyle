import { LibItem, LibraryCategory } from "../../../models";
import { LibraryState } from "../../../redux/store/library/types";

const GetLibCategories = (state: LibraryState) => {
  const allCategories = [] as LibraryCategory[];
  const items: LibItem[] = Array.prototype.concat(state.nodeTypes, state.interfaceTypes, state.transportTypes);

  const result = items.reduce((r, a) => {
    r[a?.category] = r[a?.category] || [];
    r[a?.category].push(a);
    return r;
  }, Object.create([]));

  const objectArray = Object.entries(result);

  objectArray.forEach(([key, value]) => {
    const libCategory: LibraryCategory = {
      name: key,
      nodes: value as LibItem[],
    };

    libCategory.nodes.length > 0 && allCategories.push(libCategory);
  });

  return allCategories;
};

export default GetLibCategories;
