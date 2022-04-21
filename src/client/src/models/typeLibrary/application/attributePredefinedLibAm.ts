import { Aspect } from "../enums/aspect";

export interface AttributePredefinedLibAm {
  key: string;
  isMultiSelect: boolean;
  valueStringList: string[];
  aspect: Aspect;
}
