import { Aspect } from "../enums/aspect";
import { AttributeLibCm } from "./attributeLibCm";
import { TerminalLibCm } from "./terminalLibCm";

export interface TransportLibCm {
  id: string;
  iri: string;
  name: string;
  rdsName: string;
  rdsCode: string;
  purposeName: string;
  parentId: string;
  parent: TransportLibCm;
  version: string;
  firstVersionId: string;
  aspect: Aspect;
  companyId: number;
  description: string;
  updatedBy: string;
  updated: string | null;
  created: string;
  createdBy: string;
  terminalId: string;
  terminal: TerminalLibCm;
  attributes: AttributeLibCm[];
  kind: string;
}
