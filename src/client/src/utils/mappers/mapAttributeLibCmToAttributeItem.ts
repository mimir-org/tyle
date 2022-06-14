import { AttributeLibCm } from "@mimirorg/typelibrary-types";
import { AttributeItem } from "../../content/home/types/AttributeItem";

export const mapAttributeLibCmToAttributeItem = (attribute: AttributeLibCm): AttributeItem => ({
  id: attribute.id,
  name: attribute.name,
  traits: {
    condition: attribute.attributeCondition,
    qualifier: attribute.attributeQualifier,
    source: attribute.attributeSource,
  },
});

export const mapAttributeLibCmsToAttributeItems = (attributes: AttributeLibCm[]): AttributeItem[] =>
  attributes.map(mapAttributeLibCmToAttributeItem);
