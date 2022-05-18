import { SearchItem } from "../../types/SearchItem";

export const filterSearchItem = (node: SearchItem, query: string) =>
  node.name.toLowerCase().includes(query.toLowerCase());
