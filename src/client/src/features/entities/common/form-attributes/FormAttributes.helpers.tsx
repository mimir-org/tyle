import { AttributeLibAm, AttributeLibCm } from "@mimirorg/typelibrary-types";
import { UpdateEntity } from "common/types/updateEntity";
import { mapAttributeLibCmsToInfoItems } from "common/utils/mappers";
import { ValueObject } from "features/entities/types/valueObject";

export const onAddAttributes = (
  selectedIds: string[],
  allAttributes: AttributeLibCm[],
  append: (item: ValueObject<string>) => void
) => {
  selectedIds.forEach((id) => {
    const targetAttribute = allAttributes.find((x) => x.id === id);
    if (targetAttribute) {
      append({ value: targetAttribute.id });
    }
  });
};

export const resolveSelectedAndAvailableAttributes = (
  fieldAttributes: ValueObject<string>[],
  allAttributes: AttributeLibCm[]
) => {
  const selectedSet = new Set<string>();
  fieldAttributes.forEach((x) => selectedSet.add(x.value));

  const selected: AttributeLibCm[] = [];
  const available: AttributeLibCm[] = [];
  allAttributes.forEach((x) => {
    selectedSet.has(x.id) ? selected.push(x) : available.push(x);
  });

  return [mapAttributeLibCmsToInfoItems(available), mapAttributeLibCmsToInfoItems(selected)];
};
