import { UseFieldArrayReturn } from "react-hook-form";
import { FormNodeLib } from "../types/formNodeLib";
import { AttributeLibCm } from "../../../models/tyle/client/attributeLibCm";
import { mapAttributeLibCmToAttributeItem } from "../../../utils/mappers";
import { Aspect } from "../../../models/tyle/enums/aspect";

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

export const filterAttributes = (attributes?: AttributeLibCm[], aspects?: Aspect[]) => {
  if (!attributes || attributes.length == 0) return [];
  if (!aspects || aspects.length == 0) return [];

  return attributes.filter((a) => aspects.some((x) => x === a.aspect));
};
