import { InfoItem } from "types/infoItem";
import { Direction } from "./terminals/direction";

export type BlockTerminalItemDirection = "Input" | "Output" | "Bidirectional";

export interface BlockTerminalItem {
  id: string;
  name: string;
  maxQuantity?: number;
  color: string;
  direction: Direction;
  attributes?: InfoItem[];
}
