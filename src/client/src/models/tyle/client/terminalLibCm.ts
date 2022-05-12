import { AttributeLibCm } from "./attributeLibCm";

export interface TerminalLibCm {
  id: string;
  iri: string;
  contentReferences: string[];
  name: string;
  parentIri: string;
  parentName: string;
  version: string;
  firstVersionId: string;
  color: string;
  description: string;
  updatedBy: string;
  updated: string | null;
  created: string;
  createdBy: string;
  attributes: AttributeLibCm[];
  children: TerminalLibCm[];
}
