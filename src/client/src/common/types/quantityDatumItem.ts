import { StateItem } from "common/types/stateItem";
import { QuantityDatumType, State } from "@mimirorg/typelibrary-types";

export interface QuantityDatumItem extends StateItem {
  id: string;
  name: string;
  description: string;
  quantityDatumType: QuantityDatumType;
  typeReference: string;
  state: State;
  kind: string;
}
