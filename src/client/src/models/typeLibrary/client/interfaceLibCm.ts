import { Aspect } from "../enums/aspect";
import { AttributeLibCm } from "./attributeLibCm";
import { PurposeLibCm } from "./purposeLibCm";
import { RdsLibCm } from "./rdsLibCm";
import { TerminalLibCm } from "./terminalLibCm";

export interface InterfaceLibCm {
  id: string;
  iri: string;
  name: string;
  rdsId: string;
  rds: RdsLibCm;
  purposeId: string;
  purpose: PurposeLibCm;
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
