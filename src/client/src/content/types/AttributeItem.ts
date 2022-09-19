import { InfoItem } from "./InfoItem";

export interface AttributeItem {
  id: string;
  name: string;
  description: string;
  color: string;
  quantityDatumSpecifiedScope: string;
  quantityDatumSpecifiedProvenance: string;
  quantityDatumRangeSpecifying: string;
  quantityDatumRegularitySpecified: string;
  tokens: string[];
  contents: InfoItem[];
  kind: string;
}
