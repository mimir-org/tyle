import { InfoItem } from "common/types/infoItem";
import { BlockTerminalItem } from "common/types/blockTerminalItem";
import { StateItem } from "common/types/stateItem";

export interface BlockItem extends StateItem {
  id: string;
  name: string;
  description: string;
  img: string;
  color: string;
  tokens: string[];
  terminals: BlockTerminalItem[];
  attributes: InfoItem[];
  kind: string;
  createdBy: string;
}
