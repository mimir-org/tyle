import { StateItem } from "common/types/stateItem";

export interface UnitItem extends StateItem {
  id: string;
  iri: string;
  name: string;
  typeReference: string;
  created: Date;
  createdBy: string;
  description: string;
  kind: string;
  symbol: string;
}
