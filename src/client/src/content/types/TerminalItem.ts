import { AttributeItem } from "./AttributeItem";

export type TerminalItemDirection = "Input" | "Output" | "Bidirectional";

export interface TerminalItem {
  name: string;
  amount: number;
  color: string;
  direction: TerminalItemDirection;
  attributes?: AttributeItem[];
}
