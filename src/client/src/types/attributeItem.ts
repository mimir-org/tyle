import { StateItem } from "types/stateItem";
import { RdlUnit } from "./attributes/rdlUnit";

export interface AttributeItem extends StateItem {
  id: string;
  name: string;
  created: Date;
  createdBy: string;
  description: string;
  attributeUnits: RdlUnit[];
  kind: string;
  symbol: string;
  unitId: string;
  isDefault: boolean;
}
