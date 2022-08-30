import { InfoItem } from "./InfoItem";
import { TerminalItem } from "./TerminalItem";

export interface NodeItem {
  id: string;
  name: string;
  description: string;
  img: string;
  color: string;
  tokens: string[];
  terminals: TerminalItem[];
  attributes: InfoItem[];
}
