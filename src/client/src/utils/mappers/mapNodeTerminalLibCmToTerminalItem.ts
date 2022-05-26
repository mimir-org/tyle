import { NodeTerminalLibCm } from "../../models/tyle/client/nodeTerminalLibCm";
import { TerminalItem } from "../../content/home/types/TerminalItem";
import { ConnectorDirection } from "../../models/tyle/enums/connectorDirection";
import { mapAttributeLibCmsToAttributeItems } from "./mapAttributeLibCmToAttributeItem";
import { sortAttributes } from "../sorters";

export const mapNodeTerminalLibCmToTerminalItem = (terminal: NodeTerminalLibCm): TerminalItem => ({
  name: terminal.terminal.name,
  color: terminal.terminal.color,
  amount: terminal.number,
  direction: ConnectorDirection[terminal.connectorDirection] as keyof typeof ConnectorDirection,
  attributes: sortAttributes(mapAttributeLibCmsToAttributeItems(terminal.terminal.attributes)),
});

export const mapNodeTerminalLibCmsToTerminalItems = (terminals: NodeTerminalLibCm[]): TerminalItem[] =>
  terminals.map((x) => mapNodeTerminalLibCmToTerminalItem(x));
