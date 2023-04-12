import { InfoItem } from "common/types/infoItem";
import { AspectObjectTerminalItem } from "common/types/aspectObjectTerminalItem";
import { StateItem } from "common/types/stateItem";

export interface AspectObjectItem extends StateItem {
  id: number;
  name: string;
  description: string;
  img: string;
  color: string;
  tokens: string[];
  terminals: AspectObjectTerminalItem[];
  attributes: InfoItem[];
  kind: string;
}
