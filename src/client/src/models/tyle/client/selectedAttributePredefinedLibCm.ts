import { Aspect } from "../enums/aspect";

export interface SelectedAttributePredefinedLibCm {
  key: string;
  iri: string;
  contentReferences: string[];
  isMultiSelect: boolean;
  values: { [key: string]: boolean };
  aspect: Aspect;
  kind: string;
}
