import { AttributeLibCm } from "@mimirorg/typelibrary-types";
import { Text } from "../../../complib/text";
import { InfoItem } from "../../types/infoItem";

export const mapAttributeLibCmToInfoItem = (attribute: AttributeLibCm): InfoItem => {
  const infoItem = {
    id: attribute.id,
    name: attribute.name,
    descriptors: {
      IRI: (
        <Text
          as={"a"}
          href={attribute.iri}
          target={"_blank"}
          rel={"noopener noreferrer"}
          variant={"body-small"}
          color={"inherit"}
        >
          {attribute.iri}
        </Text>
      ),
    },
  };

  const attributeHasAttributes = attribute.units && attribute.units.length > 0;
  if (attributeHasAttributes) {
    return {
      ...infoItem,
      descriptors: {
        ...infoItem.descriptors,
        Units: attribute.units
          .map((x) => x.name)
          .sort((a, b) => a.localeCompare(b))
          .join(", "),
      },
    };
  }

  return infoItem;
};

export const mapAttributeLibCmsToInfoItems = (attributes: AttributeLibCm[]): InfoItem[] =>
  attributes.map(mapAttributeLibCmToInfoItem);
