import { AttributeLibCm } from "@mimirorg/typelibrary-types";

export const prepareAttributes = (attributes?: AttributeLibCm[]) => {
  if (!attributes || attributes.length === 0) return [];

  return attributes.sort((a, b) => a.name.localeCompare(b.name));
};
