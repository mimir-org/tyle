import { AttributeLibCm } from "./attributeLibCm";

export interface SimpleLibCm {
  id: string;
  name: string;
  iri: string;
  contentReferences: string[];
  description: string;
  updatedBy: string;
  updated: string | null;
  created: string;
  createdBy: string;
  attributes: AttributeLibCm[];
  kind: string;
}
