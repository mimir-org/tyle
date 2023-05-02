import { AttributeLibAm, AttributeLibCm } from "@mimirorg/typelibrary-types";

export const toAttributeLibAm = (attribute: AttributeLibCm): AttributeLibAm => ({
  ...attribute,
  attributeUnits: attribute.attributeUnits.map((x) => ({ unitId: x.unit.id, isDefault: x.isDefault })),
  description: attribute.description,
  name: attribute.name,
  typeReference: attribute.typeReference,
});

export const createEmptyAttribute = (): AttributeLibAm => ({
  attributeUnits: [],
  name: "",
  typeReference: "",
  description: "",
});
