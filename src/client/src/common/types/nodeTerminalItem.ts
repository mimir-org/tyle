import { InfoItem } from "common/types/infoItem";

export type NodeTerminalItemDirection = "Input" | "Output" | "Bidirectional";

export interface NodeTerminalItem {
  name: string;
  maxQuantity: number;
  color: string;
  direction: NodeTerminalItemDirection;
  attributes?: InfoItem[];
}
