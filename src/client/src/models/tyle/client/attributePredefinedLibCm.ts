import { Aspect } from "../enums/aspect";

export interface AttributePredefinedLibCm {
  key: string;
  iri: string;
  contentReferences: string[];
  isMultiSelect: boolean;
  valueStringList: string[];
  aspect: Aspect;
  updatedBy: string;
  updated: string | null;
  created: string;
  createdBy: string;
  kind: string;
}
