import { AttributeItem } from "../../common/types/attributeItem";
import { AttributeView } from "common/types/attributes/attributeView";

export const toAttributeItem = (attribute: AttributeView): AttributeItem => {
  return {
    attributeUnits: attribute.units,
    created: attribute.createdOn,
    createdBy: attribute.createdBy,
    id: attribute.id,
    name: attribute.name,
    description: attribute.description ?? "",
    kind: "AttributeItem",
    state: attribute.state,
    symbol: "",
    unitId: "",
    isDefault: false,
  };
};
