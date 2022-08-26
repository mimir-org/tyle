import { AttributeLibCm } from "@mimirorg/typelibrary-types";
import { InfoItem } from "../../content/types/InfoItem";

export const mapAttributeLibCmToInfoItem = (attribute: AttributeLibCm): InfoItem => ({
  id: attribute.id,
  name: attribute.name,
  descriptors: {
    condition: attribute.attributeCondition,
    qualifier: attribute.attributeQualifier,
    source: attribute.attributeSource,
  },
});

export const mapAttributeLibCmsToInfoItems = (attributes: AttributeLibCm[]): InfoItem[] =>
  attributes.map(mapAttributeLibCmToInfoItem);
