import { BlockTerminalItem } from "types/blockTerminalItem";
import { InfoItem } from "types/infoItem";
import { StateItem } from "types/stateItem";

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
