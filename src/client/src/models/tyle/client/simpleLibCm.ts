import { AttributeLibCm } from "./attributeLibCm";

export interface SimpleLibCm {
  id: string;
  name: string;
  iri: string;
  description: string;
  attributes: AttributeLibCm[];
  kind: string;
}
