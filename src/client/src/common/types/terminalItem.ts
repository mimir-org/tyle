import { InfoItem } from "./infoItem";

export interface TerminalItem {
  id: string;
  name: string;
  color: string;
  description: string;
  attributes: InfoItem[];
  tokens: string[];
  kind: string;
}
