import { AttributeLibCm } from "@mimirorg/typelibrary-types";
import { AttributeItem } from "../../types/attributeItem";

export const toAttributeItem = (attribute: AttributeLibCm): AttributeItem => {
  return {
    attributeUnits: attribute.attributeUnits,
    created: attribute.created,
    createdBy: attribute.createdBy,
    iri: attribute.iri,
    typeReference: attribute.typeReference,
    id: attribute.id,
    name: attribute.name,
    description: attribute.description,
    kind: "AttributeItem",
    state: attribute.state,
    symbol: "",
    unitId: "",
    isDefault: false,
  };
};
