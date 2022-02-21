import { Aspect } from "../enums/aspect";
import { AttributeLibCm } from "./attributeLibCm";
import { ObjectType } from "../enums/objectType";
import { PurposeLibCm } from "./purposeLibCm";

export interface NodeLibCm {
  id: string;
  parentId: string;
  version: string;
  rds: string;
  category: string;
  name: string;
  description: string;
  statusId: string;
  aspect: Aspect;
  attributes: AttributeLibCm[];
  iri: string;
  blobId: string;
  libraryType: ObjectType;
  purpose: PurposeLibCm;
  updatedBy: string;
  updated: string | null;
  created: string;
  createdBy: string;
  kind: string;
}
