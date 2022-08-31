import { InfoItem } from "./InfoItem";

export interface AttributeItem {
  id: string;
  name: string;
  description: string;
  color: string;
  qualifier: string;
  source: string;
  condition: string;
  tokens: string[];
  contents: InfoItem[];
  kind: string;
}
