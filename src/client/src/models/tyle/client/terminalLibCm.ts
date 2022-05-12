import { AttributeLibCm } from "./attributeLibCm";

export interface TerminalLibCm {
  id: string;
  name: string;
  iri: string;
  parentIri: string;
  parentName: string;
  color: string;
  description: string;
  attributes: AttributeLibCm[];
  children: TerminalLibCm[];
}
