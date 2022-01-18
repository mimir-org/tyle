import { LibraryCategory } from "../../../models";

export const GetFilteredLibCategories = (libCategories: LibraryCategory[], searchString: string): LibraryCategory[] => {
  const searchStringLower = searchString.toLowerCase();

  if (searchString === "") return [];

  return libCategories
    .map((cat) => {
      return { ...cat, nodes: cat.nodes.filter((libItem) => libItem.name.toLowerCase().includes(searchStringLower)) };
    })
    .filter((cat) => cat.nodes.some((y) => y.name.toLowerCase().includes(searchStringLower)));
};
