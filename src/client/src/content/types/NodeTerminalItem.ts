import { InfoItem } from "./InfoItem";

export type NodeTerminalItemDirection = "Input" | "Output" | "Bidirectional";

export interface NodeTerminalItem {
  name: string;
  amount: number;
  color: string;
  direction: NodeTerminalItemDirection;
  attributes?: InfoItem[];
}
