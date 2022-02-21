import { Aspect } from "../enums/aspect";
import { ObjectType } from "../enums/objectType";
import { PurposeLibCm } from "./purposeLibCm";

export interface InterfaceLibCm {
  id: string;
  parentId: string;
  version: string;
  rds: string;
  category: string;
  aspect: Aspect;
  name: string;
  description: string;
  iri: string;
  terminalId: string;
  libraryType: ObjectType;
  purpose: PurposeLibCm;
  updatedBy: string;
  updated: string | null;
  created: string;
  createdBy: string;
  kind: string;
}
