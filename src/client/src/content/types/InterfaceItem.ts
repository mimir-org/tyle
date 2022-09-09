import { InfoItem } from "./InfoItem";
import { TerminalItem } from "./TerminalItem";

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
