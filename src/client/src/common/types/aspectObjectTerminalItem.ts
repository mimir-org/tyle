import { InfoItem } from "common/types/infoItem";

export type AspectObjectTerminalItemDirection = "Input" | "Output" | "Bidirectional";

export interface AspectObjectTerminalItem {
  name: string;
  maxQuantity: number;
  color: string;
  direction: AspectObjectTerminalItemDirection;
  attributes?: InfoItem[];
}