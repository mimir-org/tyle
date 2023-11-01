import { AttributeView } from "types/attributes/attributeView";

export const prepareAttributes = (attributes?: AttributeView[]) => {
  if (!attributes || attributes.length === 0) return [];

  return attributes.sort((a, b) => a.name.localeCompare(b.name));
};
