import { InfoItem } from "common/types/infoItem";
import { StateItem } from "common/types/stateItem";

export interface TerminalItem extends StateItem {
  id: string;
  name: string;
  color: string;
  description: string;
  attributes: InfoItem[];
  tokens: string[];
  kind: string;
  createdBy: string;
}
