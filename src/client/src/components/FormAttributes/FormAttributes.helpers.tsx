import { AttributeView } from "types/attributes/attributeView";
import { AttributeTypeReferenceView } from "types/common/attributeTypeReferenceView";
import { mapAttributeViewsToInfoItems } from "./mapAttributeLibCmToInfoItem";

export const onAddAttributes = (
  selectedIds: string[],
  allAttributes: AttributeView[],
  append: (item: AttributeTypeReferenceView) => void,
) => {
  selectedIds.forEach((id) => {
    const targetAttribute = allAttributes.find((x) => x.id === id);
    if (targetAttribute) {
      append({ attribute: targetAttribute, minCount: 1, maxCount: null });
    }
  });
};

export const resolveSelectedAndAvailableAttributes = (
  fieldAttributes: AttributeTypeReferenceView[],
  allAttributes: AttributeView[],
) => {
  const selectedSet = new Set<string>();
  fieldAttributes.forEach((x) => selectedSet.add(x.attribute.id));

  const selected: AttributeView[] = [];
  const available: AttributeView[] = [];
  allAttributes.forEach((x) => {
    selectedSet.has(x.id) ? selected.push(x) : available.push(x);
  });

  return [mapAttributeViewsToInfoItems(available), mapAttributeViewsToInfoItems(selected)];
};
