import { InfoItem } from "./InfoItem";
import { NodeTerminalItem } from "./NodeTerminalItem";

export interface NodeItem {
  id: string;
  name: string;
  description: string;
  img: string;
  color: string;
  tokens: string[];
  terminals: NodeTerminalItem[];
  attributes: InfoItem[];
  kind: string;
}
