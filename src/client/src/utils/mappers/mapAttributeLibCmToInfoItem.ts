import { AttributeLibCm } from "@mimirorg/typelibrary-types";
import { InfoItem } from "../../content/types/InfoItem";

export const mapAttributeLibCmToInfoItem = (attribute: AttributeLibCm): InfoItem => ({
  id: attribute.id,
  name: attribute.name,
  descriptors: {
    quantityDatumSpecifiedScope: attribute.quantityDatumSpecifiedScope,
    quantityDatumSpecifiedProvenance: attribute.quantityDatumSpecifiedProvenance,
    quantityDatumRangeSpecifying: attribute.quantityDatumRangeSpecifying,
    quantityDatumRegularitySpecified: attribute.quantityDatumRegularitySpecified,
  },
});

export const mapAttributeLibCmsToInfoItems = (attributes: AttributeLibCm[]): InfoItem[] =>
  attributes.map(mapAttributeLibCmToInfoItem);
