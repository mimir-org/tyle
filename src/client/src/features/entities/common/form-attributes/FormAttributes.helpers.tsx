import { AttributeLibAm, AttributeLibCm } from "@mimirorg/typelibrary-types";
import { UpdateEntity } from "../../../../data/types/updateEntity";
import { mapAttributeLibCmsToInfoItems } from "../../../../utils/mappers";
import { ValueObject } from "../../types/valueObject";

export const onAddAttributes = (
  selectedIds: string[],
  availableAttributes: AttributeLibCm[],
  fields: ValueObject<UpdateEntity<AttributeLibAm>>[],
  append: (item: ValueObject<UpdateEntity<AttributeLibAm>>) => void
) => {
  selectedIds.forEach((id) => {
    const attributeHasNotBeenAdded = !fields.some((x) => x.value.id === id);
    if (attributeHasNotBeenAdded) {
      const targetAttribute = availableAttributes.find((x) => x.id === id);
      if (targetAttribute) {
        append({ value: targetAttribute });
      }
    }
  });
};

export const getInfoItemsFromAttributeLibCms = (attributes?: AttributeLibCm[]) => {
  if (!attributes || attributes.length == 0) return [];

  return mapAttributeLibCmsToInfoItems(attributes);
};
