import { Aspect } from "../enums/aspect";
import { AttributeLibCm } from "./attributeLibCm";
import { TerminalLibCm } from "./terminalLibCm";

export interface InterfaceLibCm {
  id: string;
  iri: string;
  name: string;
  rdsId: string;
  rdsName: string;
  purposeId: string;
  purposeName: string;
  parentId: string;
  parent: InterfaceLibCm;
  version: string;
  firstVersionId: string;
  aspect: Aspect;
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
