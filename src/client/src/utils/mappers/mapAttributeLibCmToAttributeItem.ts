import { AttributeLibCm } from "@mimirorg/typelibrary-types";
import { AttributeItem } from "../../content/types/AttributeItem";
import { InfoItem } from "../../content/types/InfoItem";
import { getColorFromAspect } from "../getColorFromAspect";
import { mapAttributeLibToQuantityDatumDescriptors } from "./mapAttributeLibToQuantityDatumDescriptors";
import { mapListToDescriptors } from "./mapListToDescriptors";
import { mapTypeReferencesToDescriptors } from "./mapTypeReferencesToDescriptors";
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

  const quantityDatumDescriptors = mapAttributeLibToQuantityDatumDescriptors(attribute);
  if (Object.keys(quantityDatumDescriptors).length > 0) {
    contents.push({
      name: "Datum",
      descriptors: quantityDatumDescriptors,
    });
  }

  if (attribute.typeReferences.length > 0) {
    contents.push({
      name: "References",
      descriptors: mapTypeReferencesToDescriptors(attribute.typeReferences),
    });
  }

  return {
    id: attribute.id,
    name: attribute.name,
    description: attribute.description,
    color: getColorFromAspect(attribute.aspect),
    tokens: [attribute.createdBy, attribute.companyName],
    contents: contents,
    kind: "AttributeItem",
  };
};
