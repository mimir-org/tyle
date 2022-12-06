import { ConnectorDirection, NodeLibCm, NodeTerminalLibCm, State } from "@mimirorg/typelibrary-types";
import { NodeItem } from "common/types/nodeItem";
import { NodeTerminalItem } from "common/types/nodeTerminalItem";
import { getColorFromAspect } from "common/utils/getColorFromAspect";
import { getOptionsFromEnum } from "common/utils/getOptionsFromEnum";
import { mapAttributeLibCmsToInfoItems } from "common/utils/mappers/mapAttributeLibCmToInfoItem";
import { sortInfoItems } from "common/utils/sorters";

export const mapNodeLibCmToNodeItem = (node: NodeLibCm): NodeItem => {
  const states = getOptionsFromEnum(State);
  const currentStateLabel = states[node.state].label;

  return {
    id: node.id,
    name: node.name,
    img: node.symbol,
    description: node.description,
    color: getColorFromAspect(node.aspect),
    tokens: [node.version, node.companyName, currentStateLabel, node.rdsName, node.purposeName],
    terminals: sortNodeTerminals(mapNodeTerminalLibCmsToNodeTerminalItems(node.nodeTerminals)),
    attributes: sortInfoItems(mapAttributeLibCmsToInfoItems(node.attributes)),
    kind: "NodeItem",
    state: node.state,
    companyId: node.companyId
  };
};

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
