import { InfoItem } from "types/infoItem";
import { Direction } from "./terminals/direction";

export interface BlockTerminalItem {
  id: string;
  name: string;
  minQuantity: number;
  maxQuantity?: number;
  color: string;
  direction: Direction;
  attributes?: InfoItem[];
}
