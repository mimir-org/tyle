import { AttributeLibAm, AttributeLibCm } from "@mimirorg/typelibrary-types";

export const mapAttributeLibCmToAttributeLibAm = (attribute: AttributeLibCm): AttributeLibAm => ({
  ...attribute,
  unitIdList: attribute.units.map((x) => x.id),
});
