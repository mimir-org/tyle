import { AttributeLibCm, State } from "@mimirorg/typelibrary-types";

export interface AttributeGroupItem {
  id: string;
  name: string;
  created: Date;
  createdBy: string;
  description: string;
  kind: string;
  attributeIds: string[];
  attributes: AttributeLibCm[];
  state: State;
}
