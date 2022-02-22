import { Aspect } from "../enums/aspect";

export interface AttributeAspectLibCm {
  id: string;
  parentId: string;
  parent: AttributeAspectLibCm;
  name: string;
  iri: string;
  aspect: Aspect;
  description: string;
  children: AttributeAspectLibCm[];
  updatedBy: string;
  updated: string | null;
  created: string;
  createdBy: string;
  kind: string;
}
