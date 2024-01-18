import { BlockTerminalItem } from "types/blockTerminalItem";
import { InfoItem } from "types/infoItem";
import { StateItem } from "types/stateItem";
import { EngineeringSymbol } from "./blocks/engineeringSymbol";

export interface BlockItem extends StateItem {
  id: string;
  name: string;
  description: string;
  symbol: EngineeringSymbol | null;
  color: string;
  tokens: string[];
  terminals: BlockTerminalItem[];
  attributes: InfoItem[];
  kind: string;
  createdBy: string;
}
