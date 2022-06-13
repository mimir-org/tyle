import { NodeLibCm } from "../../models/tyle/client/nodeLibCm";
import { NodeItem } from "../../content/home/types/NodeItem";
import { getColorFromAspect } from "../getColorFromAspect";
import { mapNodeTerminalLibCmsToTerminalItems } from "./mapNodeTerminalLibCmToTerminalItem";
import { mapAttributeLibCmsToAttributeItems } from "./mapAttributeLibCmToAttributeItem";
import { sortAttributes, sortTerminals } from "../sorters";

export const mapNodeLibCmToNodeItem = (node: NodeLibCm): NodeItem => ({
  id: node.id,
  name: node.name,
  img: node.symbol,
  description: node.description,
  color: getColorFromAspect(node.aspect),
  tokens: [node.rdsName, node.purposeName],
  terminals: sortTerminals(mapNodeTerminalLibCmsToTerminalItems(node.nodeTerminals)),
  attributes: sortAttributes(mapAttributeLibCmsToAttributeItems(node.attributes)),
});
