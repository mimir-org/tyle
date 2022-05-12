import { Aspect } from "../enums/aspect";

export interface AttributeAspectLibCm {
  id: string;
  name: string;
  iri: string;
  parentIri: string;
  parentName: string;
  aspect: Aspect;
  description: string;
  children: AttributeAspectLibCm[];
  updatedBy: string;
  updated: string | null;
  created: string;
  createdBy: string;
  kind: string;
}
