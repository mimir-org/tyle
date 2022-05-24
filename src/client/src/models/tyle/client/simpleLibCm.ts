import { AttributeLibCm } from "./attributeLibCm";

export interface SimpleLibCm {
  id: string;
  name: string;
  iri: string;
  contentReferences: string[];
  description: string;
  created: string;
  createdBy: string;
  attributes: AttributeLibCm[];
  kind: string;
}
