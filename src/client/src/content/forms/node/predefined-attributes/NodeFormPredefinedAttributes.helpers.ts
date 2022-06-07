import { AttributePredefinedLibCm } from "../../../../models/tyle/client/attributePredefinedLibCm";
import { Aspect } from "../../../../models/tyle/enums/aspect";

export const preparePredefinedAttributes = (predefinedAttributes?: AttributePredefinedLibCm[], aspects?: Aspect[]) => {
  if (!predefinedAttributes || predefinedAttributes.length == 0) return [];
  if (!aspects || aspects.length == 0) return [];

  return predefinedAttributes.filter((a) => aspects.some((x) => x === a.aspect));
};
