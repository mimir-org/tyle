import { InfoItem } from "common/types/infoItem";
import { TerminalItem } from "common/types/terminalItem";
import { StateItem } from "common/types/stateItem";

export interface InterfaceItem extends StateItem {
  id: string;
  name: string;
  aspectColor: string;
  interfaceColor: string;
  description: string;
  attributes: InfoItem[];
  terminal: TerminalItem;
  tokens: string[];
  kind: string;
}
