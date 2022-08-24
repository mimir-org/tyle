import { Aspect, AttributeLibCm, AttributeType } from "@mimirorg/typelibrary-types";
import { UseFieldArrayReturn } from "react-hook-form";
import { mapAttributeLibCmToAttributeItem } from "../../../../utils/mappers";
import { FormNodeLib } from "../../types/formNodeLib";

export const onAddAttributes = (
  ids: string[],
  attributeFields: UseFieldArrayReturn<FormNodeLib, "attributeIdList">
) => {
  ids.forEach((id) => {
    const attributeHasNotBeenAdded = !attributeFields.fields.some((f) => f.value === id);
    if (attributeHasNotBeenAdded) {
      attributeFields.append({ value: id });
    }
  });
};

export const getAttributeItems = (attributes?: AttributeLibCm[]) => {
  if (!attributes || attributes.length == 0) return [];

  return attributes.map((x) => mapAttributeLibCmToAttributeItem(x));
};

export const prepareAttributes = (attributes?: AttributeLibCm[], aspects?: Aspect[]) => {
  if (!attributes || attributes.length == 0) return [];
  if (!aspects || aspects.length == 0) return [];

  return attributes
    .filter((a) => aspects.some((x) => x === a.aspect && a.attributeType === AttributeType.Normal))
    .sort((a, b) => a.discipline - b.discipline);
};
