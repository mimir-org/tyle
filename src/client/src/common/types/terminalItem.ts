import { InfoItem } from "common/types/infoItem";

export interface TerminalItem {
  id: string;
  name: string;
  color: string;
  description: string;
  attributes: InfoItem[];
  tokens: string[];
  kind: string;
}
