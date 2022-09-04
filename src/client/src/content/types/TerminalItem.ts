import { InfoItem } from "./InfoItem";

export type TerminalItemDirection = "Input" | "Output" | "Bidirectional";

export interface TerminalItem {
  name: string;
  amount: number;
  color: string;
  direction: TerminalItemDirection;
  attributes?: InfoItem[];
}
