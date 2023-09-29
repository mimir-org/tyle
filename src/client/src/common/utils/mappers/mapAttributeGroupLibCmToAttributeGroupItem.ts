import { AttributeGroupLibCm, State } from "@mimirorg/typelibrary-types";
import { AttributeGroupItem } from "common/types/attributeGroupItem";

export const toAttributeGroupItem = (attribute: AttributeGroupLibCm): AttributeGroupItem => {
  return {
    id: attribute.id,
    name: attribute.name,
    created: attribute.created,
    createdBy: attribute.createdBy,
    description: attribute.description,
    kind: "AttributeGroupItem",
    attributeIds: attribute.attributes.map((x) => x.id),
    state: State.Draft,
    attributes: attribute.attributes,
  };
};
