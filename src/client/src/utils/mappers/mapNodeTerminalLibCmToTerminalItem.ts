import { ConnectorDirection, NodeTerminalLibCm } from "@mimirorg/typelibrary-types";
import { TerminalItem } from "../../content/types/TerminalItem";
import { sortInfoItems } from "../sorters";
import { mapAttributeLibCmsToInfoItems } from "./mapAttributeLibCmToInfoItem";

export const mapNodeTerminalLibCmToTerminalItem = (terminal: NodeTerminalLibCm): TerminalItem => ({
  name: terminal.terminal.name,
  color: terminal.terminal.color,
  amount: terminal.quantity,
  direction: ConnectorDirection[terminal.connectorDirection] as keyof typeof ConnectorDirection,
  attributes: sortInfoItems(mapAttributeLibCmsToInfoItems(terminal.terminal.attributes)),
});

export const mapNodeTerminalLibCmsToTerminalItems = (terminals: NodeTerminalLibCm[]): TerminalItem[] =>
  terminals.map((x) => mapNodeTerminalLibCmToTerminalItem(x));
