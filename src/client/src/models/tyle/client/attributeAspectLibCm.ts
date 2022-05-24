import { Aspect } from "../enums/aspect";

export interface AttributeAspectLibCm {
  id: string;
  parentName: string;
  parentIri: string;
  name: string;
  iri: string;
  contentReferences: string[];
  aspect: Aspect;
  description: string;
  created: string;
  createdBy: string;
  children: AttributeAspectLibCm[];
  kind: string;
}
