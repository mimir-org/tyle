import { InfoItem } from "./InfoItem";

export interface TransportItem {
  id: string;
  name: string;
  color: string;
  description: string;
  attributes: InfoItem[];
  tokens: string[];
  kind: string;
}
