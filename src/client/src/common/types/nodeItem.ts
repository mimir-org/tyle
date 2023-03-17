import { InfoItem } from "common/types/infoItem";
import { NodeTerminalItem } from "common/types/nodeTerminalItem";
import { StateItem } from "common/types/stateItem";

export interface NodeItem extends StateItem {
  id: number;
  name: string;
  description: string;
  img: string;
  color: string;
  tokens: string[];
  terminals: NodeTerminalItem[];
  attributes: InfoItem[];
  kind: string;
}
