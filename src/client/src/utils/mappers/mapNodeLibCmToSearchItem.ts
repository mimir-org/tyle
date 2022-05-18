import { NodeLibCm } from "../../models/tyle/client/nodeLibCm";
import { SearchItem } from "../../content/home/types/SearchItem";
import { getColorFromAspect } from "../getColorFromAspect";

export const mapNodeLibCmToSearchItem = (node: NodeLibCm): SearchItem => ({
  id: node.id,
  name: node.name,
  description: node.description,
  color: getColorFromAspect(node.aspect),
  img: node.symbol,
});
