import { AttributeLibCm } from "@mimirorg/typelibrary-types";
import { InfoItem } from "../../content/types/InfoItem";
import { mapAttributeLibToQuantityDatumDescriptors } from "./mapAttributeLibToQuantityDatumDescriptors";

export const mapAttributeLibCmToInfoItem = (attribute: AttributeLibCm): InfoItem => ({
  id: attribute.id,
  name: attribute.name,
  descriptors: mapAttributeLibToQuantityDatumDescriptors(attribute),
});

export const mapAttributeLibCmsToInfoItems = (attributes: AttributeLibCm[]): InfoItem[] =>
  attributes.map(mapAttributeLibCmToInfoItem);
