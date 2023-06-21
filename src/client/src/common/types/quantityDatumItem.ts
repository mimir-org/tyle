import { StateItem } from "common/types/stateItem";
import { QuantityDatumType } from "@mimirorg/typelibrary-types";

export interface QuantityDatumItem extends StateItem {
  id: string;
  name: string;
  description: string;
  quantityDatumType: QuantityDatumType;
  typeReference: string;
  kind: string;
  createdBy: string;
}
