import { Aspect } from "../enums/aspect";

export interface AttributeAspectLibCm {
  id: string;
  name: string;
  iri: string;
  contentReferences: string[];
  parentIri: string;
  parentName: string;
  aspect: Aspect;
  description: string;
  updatedBy: string;
  updated: string | null;
  created: string;
  createdBy: string;
  children: AttributeAspectLibCm[];
  kind: string;
}
