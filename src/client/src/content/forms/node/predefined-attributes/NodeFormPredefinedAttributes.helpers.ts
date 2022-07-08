import { Aspect, AttributePredefinedLibCm } from "@mimirorg/typelibrary-types";

export const preparePredefinedAttributes = (predefinedAttributes?: AttributePredefinedLibCm[], aspects?: Aspect[]) => {
  if (!predefinedAttributes || predefinedAttributes.length == 0) return [];
  if (!aspects || aspects.length == 0) return [];

  return predefinedAttributes.filter((a) => aspects.some((x) => x === a.aspect)).sort((x) => (x.isMultiSelect ? 1 : 0));
};
