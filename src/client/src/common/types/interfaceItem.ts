import { InfoItem } from "./infoItem";
import { TerminalItem } from "./terminalItem";

export interface InterfaceItem {
  id: string;
  name: string;
  aspectColor: string;
  interfaceColor: string;
  description: string;
  attributes: InfoItem[];
  terminal: TerminalItem;
  tokens: string[];
  kind: string;
}
