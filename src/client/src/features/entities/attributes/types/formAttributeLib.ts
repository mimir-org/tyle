import { AttributeLibAm, AttributeLibCm } from "@mimirorg/typelibrary-types";

export const mapFormAttributeLibToApiModel = (formAttribute: AttributeLibAm): AttributeLibAm => ({
  ...formAttribute,
  attributeUnits: formAttribute.attributeUnits.map((x) => x),
});

export const createEmptyFormAttributeLib = (): AttributeLibAm => ({
  ...emptyAttributeLib,
  attributeUnits: [],
});

export const mapAttributeLibCmToFormAttributeLib = (attribute: AttributeLibCm): AttributeLibAm => ({
  ...attribute,
  attributeUnits: attribute.attributeUnits.map((x) => ({ unitId: x.unit.id, isDefault: x.isDefault })),
  companyId: attribute.companyId,
  description: attribute.description,
  name: attribute.name,
  typeReference: attribute.typeReference,
});

const emptyAttributeLib: AttributeLibAm = {
  attributeUnits: [],
  name: "",
  typeReference: "",
  description: "",
  companyId: 0,
};
