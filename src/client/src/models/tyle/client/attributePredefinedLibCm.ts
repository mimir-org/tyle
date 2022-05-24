import { Aspect } from "../enums/aspect";

export interface AttributePredefinedLibCm {
  key: string;
  iri: string;
  contentReferences: string[];
  isMultiSelect: boolean;
  valueStringList: string[];
  aspect: Aspect;
  created: string;
  createdBy: string;
  kind: string;
}
