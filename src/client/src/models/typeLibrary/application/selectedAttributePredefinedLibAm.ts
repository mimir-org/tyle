import { Aspect } from "../enums/aspect";

export interface SelectedAttributePredefinedLibAm {
  key: string;
  isMultiSelect: boolean;
  values: { [key: string]: boolean };
  aspect: Aspect;
}
