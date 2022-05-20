import { NodeLibCm } from "../../models/tyle/client/nodeLibCm";
import { NodeItem } from "../../content/home/types/NodeItem";
import { getColorFromAspect } from "../getColorFromAspect";
import { mapNodeTerminalLibsToLibraryItems } from "./mapNodeTerminalLibsToLibraryItems";
import { mapAttributeLibsToAttributeItems } from "./mapAttributeLibsToAttributeItems";
import { sortAttributes, sortTerminals } from "../sorters";

export const mapNodeLibCmToNodeItem = (nodeLib: NodeLibCm): NodeItem => ({
  id: nodeLib.id,
  name: nodeLib.name,
  img: nodeLib.symbol,
  description: nodeLib.description,
  color: getColorFromAspect(nodeLib.aspect),
  tokens: [nodeLib.rdsName, nodeLib.purposeName],
  terminals: sortTerminals(mapNodeTerminalLibsToLibraryItems(nodeLib.nodeTerminals)),
  attributes: sortAttributes(mapAttributeLibsToAttributeItems(nodeLib.attributes)),
});
