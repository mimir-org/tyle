import { AttributeGroupLibCm } from "@mimirorg/typelibrary-types";
import { mapAttributeGroupLibCmsToInfoItems } from "common/utils/mappers/mapAttributeGroupLibCmToInfoItem";
import { ValueObject } from "features/entities/types/valueObject";

export const onAddAttributeGroup = (
  selectedIds: string[],
  allAttributeGroups: AttributeGroupLibCm[],
  append: (item: ValueObject<string>) => void,
) => {
  selectedIds.forEach((id) => {
    const targetAttributeGroups = allAttributeGroups.find((x) => x.id === id);
    if (targetAttributeGroups) {
      append({ value: targetAttributeGroups.id });
    }
  });
};

export const resolveSelectedAndAvailableAttributeGroups = (
  fieldAttributeGroups: ValueObject<string>[],
  allAttributeGroups: AttributeGroupLibCm[],
) => {
  const selectedSet = new Set<string>();
  fieldAttributeGroups.forEach((x) => selectedSet.add(x.value));

  const selected: AttributeGroupLibCm[] = [];
  const available: AttributeGroupLibCm[] = [];
  allAttributeGroups.forEach((x) => {
    selectedSet.has(x.id) ? selected.push(x) : available.push(x);
  });

  return [mapAttributeGroupLibCmsToInfoItems(available), mapAttributeGroupLibCmsToInfoItems(selected)];
};
