import { getColorFromAspect } from "../../../../utils/getColorFromAspect";
import { NodeLibCm } from "../../../../models/tyle/client/nodeLibCm";
import { SearchItem } from "../../types/SearchItem";

export const getNodeAsSearchItem = (node: NodeLibCm): SearchItem => ({
  id: node.id,
  name: node.name,
  description: node.description,
  color: getColorFromAspect(node.aspect),
  img: node.symbol,
});

export const filterSearchItem = (node: SearchItem, query: string) =>
  node.name.toLowerCase().includes(query.toLowerCase());
