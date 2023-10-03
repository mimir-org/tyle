import { InfoItem } from "common/types/infoItem";

export type BlockTerminalItemDirection = "Input" | "Output" | "Bidirectional";

export interface BlockTerminalItem {
  id: string;
  name: string;
  maxQuantity: number;
  color: string;
  direction: BlockTerminalItemDirection;
  attributes?: InfoItem[];
}
