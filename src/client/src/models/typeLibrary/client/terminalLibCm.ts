import { AttributeLibCm } from "./attributeLibCm";

export interface TerminalLibCm {
  id: string;
  parentId: string;
  parent: TerminalLibCm;
  name: string;
  iri: string;
  color: string;
  description: string;
  attributes: AttributeLibCm[];
}
