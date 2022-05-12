import { TerminalItem } from "./TerminalItem";

export interface NodeItem {
  name: string;
  description: string;
  img: string;
  color: string;
  tokens: string[];
  terminals: TerminalItem[];
}
