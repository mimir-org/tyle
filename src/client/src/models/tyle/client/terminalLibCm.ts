import { AttributeLibCm } from "./attributeLibCm";

export interface TerminalLibCm {
  id: string;
  parentName: string;
  parentIri: string;
  name: string;
  version: string;
  firstVersionId: string;
  iri: string;
  contentReferences: string[];
  color: string;
  description: string;
  created: string;
  createdBy: string;
  attributes: AttributeLibCm[];
  children: TerminalLibCm[];
  kind: string;
}
