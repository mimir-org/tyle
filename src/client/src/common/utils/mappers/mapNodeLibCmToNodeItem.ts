import { ConnectorDirection, NodeLibCm, NodeTerminalLibCm } from "@mimirorg/typelibrary-types";
import { NodeItem } from "common/types/nodeItem";
import { NodeTerminalItem } from "common/types/nodeTerminalItem";
import { getColorFromAspect } from "common/utils/getColorFromAspect";
import { mapAttributeLibCmsToInfoItems } from "common/utils/mappers/mapAttributeLibCmToInfoItem";
import { sortInfoItems } from "common/utils/sorters";

export const mapNodeLibCmToNodeItem = (node: NodeLibCm): NodeItem => ({
  id: node.id,
  name: node.name,
  img: node.symbol,
  description: node.description,
  color: getColorFromAspect(node.aspect),
  tokens: [node.version, node.companyName, node.rdsName, node.purposeName],
  terminals: sortNodeTerminals(mapNodeTerminalLibCmsToNodeTerminalItems(node.nodeTerminals)),
  attributes: sortInfoItems(mapAttributeLibCmsToInfoItems(node.attributes)),
  kind: "NodeItem",
});

const mapNodeTerminalLibCmsToNodeTerminalItems = (terminals: NodeTerminalLibCm[]): NodeTerminalItem[] =>
  terminals.map((x) => ({
    name: x.terminal.name,
    color: x.terminal.color,
    maxQuantity: x.maxQuantity,
    direction: ConnectorDirection[x.connectorDirection] as keyof typeof ConnectorDirection,
    attributes: sortInfoItems(mapAttributeLibCmsToInfoItems(x.terminal.attributes)),
  }));

const sortNodeTerminals = (terminals: NodeTerminalItem[]) =>
  [...terminals].sort((a, b) => a.direction.localeCompare(b.direction) || a.name.localeCompare(b.name));
