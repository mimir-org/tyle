import { InfoItem } from "./infoItem";
import { TerminalItem } from "./terminalItem";

export interface TransportItem {
  id: string;
  name: string;
  aspectColor: string;
  transportColor: string;
  description: string;
  attributes: InfoItem[];
  terminal: TerminalItem;
  tokens: string[];
  kind: string;
}
