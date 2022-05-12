import { NodeLibCm } from "../../models/tyle/client/nodeLibCm";
import { NodeItem } from "../../content/home/types/NodeItem";
import { getColorFromAspect } from "../getColorFromAspect";
import { mapNodeTerminalLibsToLibraryItems } from "./mapNodeTerminalLibsToLibraryItems";

export const mapNodeLibCmToNodeItem = (nodeLib: NodeLibCm): NodeItem => ({
  name: nodeLib.name,
  img: nodeLib.symbol,
  description: nodeLib.description,
  color: getColorFromAspect(nodeLib.aspect),
  tokens: [nodeLib.rdsName, nodeLib.purposeName],
  terminals: mapNodeTerminalLibsToLibraryItems(nodeLib.nodeTerminals),
});
