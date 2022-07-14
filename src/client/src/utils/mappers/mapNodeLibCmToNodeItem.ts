import { NodeLibCm } from "@mimirorg/typelibrary-types";
import { NodeItem } from "../../content/types/NodeItem";
import { getColorFromAspect } from "../getColorFromAspect";
import { sortAttributes, sortTerminals } from "../sorters";
import { mapAttributeLibCmsToAttributeItems } from "./mapAttributeLibCmToAttributeItem";
import { mapNodeTerminalLibCmsToTerminalItems } from "./mapNodeTerminalLibCmToTerminalItem";

export const mapNodeLibCmToNodeItem = (node: NodeLibCm): NodeItem => ({
  id: node.id,
  name: node.name,
  img: node.symbol,
  description: node.description,
  color: getColorFromAspect(node.aspect),
  tokens: [node.rdsName, node.purposeName, node.createdBy, node.version],
  terminals: sortTerminals(mapNodeTerminalLibCmsToTerminalItems(node.nodeTerminals)),
  attributes: sortAttributes(mapAttributeLibCmsToAttributeItems(node.attributes)),
});
