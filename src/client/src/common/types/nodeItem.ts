import { InfoItem } from "common/types/infoItem";
import { NodeTerminalItem } from "common/types/nodeTerminalItem";

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
