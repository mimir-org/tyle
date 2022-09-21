import { NodeLibCm } from "@mimirorg/typelibrary-types";
import { NodeItem } from "../../content/types/NodeItem";
import { getColorFromAspect } from "../getColorFromAspect";
import { sortInfoItems, sortNodeTerminals } from "../sorters";
import { mapAttributeLibCmsToInfoItems } from "./mapAttributeLibCmToInfoItem";
import { mapNodeTerminalLibCmsToNodeTerminalItems } from "./mapNodeTerminalLibCmToNodeTerminalItem";

export const mapNodeLibCmToNodeItem = (node: NodeLibCm): NodeItem => ({
  id: node.id,
  name: node.name,
  img: node.symbol,
  description: node.description,
  color: getColorFromAspect(node.aspect),
  tokens: [node.rdsName, node.purposeName, node.createdBy, node.version, node.companyName],
  terminals: sortNodeTerminals(mapNodeTerminalLibCmsToNodeTerminalItems(node.nodeTerminals)),
  attributes: sortInfoItems(mapAttributeLibCmsToInfoItems(node.attributes)),
  kind: "NodeItem",
});
