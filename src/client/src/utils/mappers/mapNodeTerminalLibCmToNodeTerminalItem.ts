import { ConnectorDirection, NodeTerminalLibCm } from "@mimirorg/typelibrary-types";
import { NodeTerminalItem } from "../../content/types/NodeTerminalItem";
import { sortInfoItems } from "../sorters";
import { mapAttributeLibCmsToInfoItems } from "./mapAttributeLibCmToInfoItem";

export const mapNodeTerminalLibCmToNodeTerminalItem = (terminal: NodeTerminalLibCm): NodeTerminalItem => ({
  name: terminal.terminal.name,
  color: terminal.terminal.color,
  amount: terminal.quantity,
  direction: ConnectorDirection[terminal.connectorDirection] as keyof typeof ConnectorDirection,
  attributes: sortInfoItems(mapAttributeLibCmsToInfoItems(terminal.terminal.attributes)),
});

export const mapNodeTerminalLibCmsToNodeTerminalItems = (terminals: NodeTerminalLibCm[]): NodeTerminalItem[] =>
  terminals.map((x) => mapNodeTerminalLibCmToNodeTerminalItem(x));
