import { AttributeLibAm, AttributeLibCm } from "@mimirorg/typelibrary-types";

type QuantityDatumProps =
  | "quantityDatumSpecifiedScope"
  | "quantityDatumSpecifiedProvenance"
  | "quantityDatumRangeSpecifying"
  | "quantityDatumRegularitySpecified";

export const mapAttributeLibToQuantityDatumDescriptors = (
  attribute: Pick<AttributeLibCm, QuantityDatumProps> | Pick<AttributeLibAm, QuantityDatumProps>
) => {
  const descriptors: { [key: string]: string } = {};

  if (attribute.quantityDatumSpecifiedProvenance) {
    descriptors["Quantity provenance"] = attribute.quantityDatumSpecifiedProvenance;
  }
  if (attribute.quantityDatumRangeSpecifying) {
    descriptors["Quantity range"] = attribute.quantityDatumRangeSpecifying;
  }
  if (attribute.quantityDatumRegularitySpecified) {
    descriptors["Quantity regularity"] = attribute.quantityDatumRegularitySpecified;
  }
  if (attribute.quantityDatumSpecifiedScope) {
    descriptors["Quantity scope"] = attribute.quantityDatumSpecifiedScope;
  }

  return descriptors;
};
