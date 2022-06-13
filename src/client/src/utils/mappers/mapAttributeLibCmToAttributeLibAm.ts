import { AttributeLibAm, AttributeLibCm } from "@mimirorg/typelibrary-types";

export const mapAttributeLibCmToAttributeLibAm = (attribute: AttributeLibCm): AttributeLibAm => ({
  ...attribute,
  parentId: attribute.parentIri,
  unitIdList: attribute.units.map((x) => x.id),
});
