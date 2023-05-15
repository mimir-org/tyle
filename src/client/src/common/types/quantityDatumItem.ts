import { StateItem } from "common/types/stateItem";
import { QuantityDatumType } from "@mimirorg/typelibrary-types";

export interface QuantityDatumItem extends StateItem {
  id: string;
  name: string;
  description: string;
  quantityType: QuantityDatumType;
  typeReference: string;
  kind: string;
}
