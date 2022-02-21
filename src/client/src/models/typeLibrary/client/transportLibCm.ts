import { Aspect } from "../enums/aspect";
import { AttributeLibCm } from "./attributeLibCm";
import { ObjectType } from "../enums/objectType";
import { PurposeLibCm } from "./purposeLibCm";

export interface TransportLibCm {
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
  attributes: AttributeLibCm[];
  libraryType: ObjectType;
  purpose: PurposeLibCm;
  updatedBy: string;
  updated: string | null;
  created: string;
  createdBy: string;
  kind: string;
}
