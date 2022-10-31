import { InfoItem } from "common/types/infoItem";
import { TerminalItem } from "common/types/terminalItem";

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
