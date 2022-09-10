import { Aspect, AttributeLibCm } from "@mimirorg/typelibrary-types";

export const prepareAttributesByAspect = (attributes?: AttributeLibCm[], aspects?: Aspect[]) => {
  if (!attributes || attributes.length == 0) return [];
  if (!aspects || aspects.length == 0) return [];

  return attributes.filter((a) => aspects.some((x) => x === a.aspect)).sort((a, b) => a.discipline - b.discipline);
};
