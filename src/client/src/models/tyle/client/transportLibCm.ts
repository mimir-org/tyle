import { Aspect } from "../enums/aspect";
import { AttributeLibCm } from "./attributeLibCm";
import { TerminalLibCm } from "./terminalLibCm";

export interface TransportLibCm {
  id: string;
  parentName: string;
  parentIri: string;
  name: string;
  version: string;
  firstVersionId: string;
  iri: string;
  contentReferences: string[];
  rdsCode: string;
  rdsName: string;
  purposeName: string;
  aspect: Aspect;
  companyId: number;
  terminalId: string;
  terminal: TerminalLibCm;
  description: string;
  created: string;
  createdBy: string;
  attributes: AttributeLibCm[];
  kind: string;
}
