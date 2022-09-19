import { AttributeLibCm } from "@mimirorg/typelibrary-types";
import { AttributeItem } from "../../content/types/AttributeItem";
import { InfoItem } from "../../content/types/InfoItem";
import { getColorFromAspect } from "../getColorFromAspect";
import { mapListToDescriptors } from "./mapListToDescriptors";
import { mapTypeReferenceCmsToDescriptors } from "./mapTypeReferenceCmsToDescriptors";
import { mapUnitLibCmsToDescriptors } from "./mapUnitLibCmsToDescriptors";

export const mapAttributeLibCmToAttributeItem = (attribute: AttributeLibCm): AttributeItem => {
  const contents: InfoItem[] = [];

  if (attribute.units.length > 0) {
    contents.push({
      name: "Units",
      descriptors: mapUnitLibCmsToDescriptors(attribute.units),
    });
  }

  if (attribute.selectValues.length > 0) {
    contents.push({
      name: "Values",
      descriptors: mapListToDescriptors(attribute.selectValues),
    });
  }

  if (attribute.typeReferences.length > 0) {
    contents.push({
      name: "References",
      descriptors: mapTypeReferenceCmsToDescriptors(attribute.typeReferences),
    });
  }

  return {
    id: attribute.id,
    name: attribute.name,
    description: attribute.description,
    color: getColorFromAspect(attribute.aspect),
    quantityDatumSpecifiedScope: attribute.quantityDatumSpecifiedScope,
    quantityDatumSpecifiedProvenance: attribute.quantityDatumSpecifiedProvenance,
    quantityDatumRangeSpecifying: attribute.quantityDatumRangeSpecifying,
    quantityDatumRegularitySpecified: attribute.quantityDatumRegularitySpecified,
    tokens: [attribute.createdBy],
    contents: contents,
    kind: "AttributeItem",
  };
};
