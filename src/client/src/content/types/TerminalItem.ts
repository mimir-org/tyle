import { AttributeItem } from "./AttributeItem";

export interface TerminalItem {
  name: string;
  amount: number;
  color: string;
  direction: "Input" | "Output" | "Bidirectional";
  attributes?: AttributeItem[];
}
