import { AttributeView } from "common/types/attributes/attributeView";
import { AttributeTypeReferenceRequest } from "common/types/common/attributeTypeReferenceRequest";
import { mapAttributeLibCmsToInfoItems } from "common/utils/mappers";

export const onAddAttributes = (
  selectedIds: string[],
  allAttributes: AttributeView[],
  append: (item: AttributeTypeReferenceRequest) => void,
) => {
  selectedIds.forEach((id) => {
    const targetAttribute = allAttributes.find((x) => x.id === id);
    if (targetAttribute) {
      append({ attributeId: targetAttribute.id, minCount: 1, maxCount: 1 });
    }
  });
};

export const resolveSelectedAndAvailableAttributes = (
  fieldAttributes: AttributeTypeReferenceRequest[],
  allAttributes: AttributeView[],
) => {
  const selectedSet = new Set<string>();
  fieldAttributes.forEach((x) => selectedSet.add(x.attributeId));

  const selected: AttributeView[] = [];
  const available: AttributeView[] = [];
  allAttributes.forEach((x) => {
    selectedSet.has(x.id) ? selected.push(x) : available.push(x);
  });

  return [mapAttributeLibCmsToInfoItems(available), mapAttributeLibCmsToInfoItems(selected)];
};
