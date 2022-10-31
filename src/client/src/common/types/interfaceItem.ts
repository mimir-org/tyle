import { InfoItem } from "common/types/infoItem";
import { TerminalItem } from "common/types/terminalItem";

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
