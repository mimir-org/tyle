import { ConnectorDirection, NodeLibCm, NodeTerminalLibCm } from "@mimirorg/typelibrary-types";
import { NodeItem } from "../../content/types/NodeItem";
import { NodeTerminalItem } from "../../content/types/NodeTerminalItem";
import { getColorFromAspect } from "../getColorFromAspect";
import { sortInfoItems, sortNodeTerminals } from "../sorters";
import { mapAttributeLibCmsToInfoItems } from "./mapAttributeLibCmToInfoItem";

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

const mapNodeTerminalLibCmsToNodeTerminalItems = (terminals: NodeTerminalLibCm[]): NodeTerminalItem[] =>
  terminals.map((x) => ({
    name: x.terminal.name,
    color: x.terminal.color,
    amount: x.quantity,
    direction: ConnectorDirection[x.connectorDirection] as keyof typeof ConnectorDirection,
    attributes: sortInfoItems(mapAttributeLibCmsToInfoItems(x.terminal.attributes)),
  }));
