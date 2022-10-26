import { InfoItem } from "./infoItem";
import { NodeTerminalItem } from "./nodeTerminalItem";

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
