import { AttributeGroupLibCm } from "@mimirorg/typelibrary-types";
import { AttributeGroupItem } from "common/types/attributeGroupItem";

export const toAttributeGroupItem = (attribute: AttributeGroupLibCm): AttributeGroupItem => {
  return {
    id: attribute.id,
    name: attribute.name,
    created: attribute.created,
    createdBy: attribute.createdBy,
    description: attribute.description,
    kind: attribute.kind,
    attributeIds: attribute.attributes.map((x) => x.id),
    state: attribute.state,
  };
};
