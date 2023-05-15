import { StateItem } from "common/types/stateItem";
import { AttributeUnitLibCm } from "@mimirorg/typelibrary-types";

export interface AttributeItem extends StateItem {
  id: string;
  iri: string;
  name: string;
  typeReference: string;
  created: Date;
  createdBy: string;
  description: string;
  attributeUnits: AttributeUnitLibCm[];
  kind: string;
  symbol: string;
  unitId: string;
  isDefault: boolean;
}
