import { AttributeLibCm } from "@mimirorg/typelibrary-types";
import { mapAttributeLibCmsToInfoItems } from "../../../utils/mappers";
import { ValueObject } from "../types/valueObject";

export const onAddValueObject = (
  ids: string[],
  fields: ValueObject<string>[],
  append: (item: ValueObject<string>) => void
) => {
  ids.forEach((id) => {
    const objectHasNotBeenAdded = !fields.some((f) => f.value === id);
    if (objectHasNotBeenAdded) append({ value: id });
  });
};

export const getSelectItemsFromAttributeLibCms = (attributes?: AttributeLibCm[]) => {
  if (!attributes || attributes.length == 0) return [];

  return mapAttributeLibCmsToInfoItems(attributes);
};
