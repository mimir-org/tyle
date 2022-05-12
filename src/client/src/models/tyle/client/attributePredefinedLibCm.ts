import { Aspect } from "../enums/aspect";

export interface AttributePredefinedLibCm {
  key: string;
  iri: string;
  isMultiSelect: boolean;
  valueStringList: string[];
  aspect: Aspect;
  kind: string;
}
