import { InfoItem } from "./InfoItem";
import { TerminalItem } from "./TerminalItem";

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
