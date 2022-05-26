import { AttributeLibCm } from "../../models/tyle/client/attributeLibCm";
import { AttributeLibAm } from "../../models/tyle/application/attributeLibAm";

export const mapAttributeLibCmToAttributeLibAm = (attribute: AttributeLibCm): AttributeLibAm => ({
  ...attribute,
  parentId: attribute.parentIri,
  unitIdList: attribute.units.map((x) => x.id),
});
