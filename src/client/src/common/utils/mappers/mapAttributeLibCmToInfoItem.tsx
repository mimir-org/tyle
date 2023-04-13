import { AttributeLibCm } from "@mimirorg/typelibrary-types";
import { InfoItem } from "common/types/infoItem";
import { Text } from "complib/text";

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

  const attributeHasAttributes = attribute.attributeUnits && attribute.attributeUnits.length > 0;
  if (attributeHasAttributes) {
    return {
      ...infoItem,
      descriptors: {
        ...infoItem.descriptors,
        Units: attribute.attributeUnits
          .map((x) => x.unit.name)
          .sort((a, b) => a.localeCompare(b))
          .join(", "),
      },
    };
  }

  return infoItem;
};

export const mapAttributeLibCmsToInfoItems = (attributes: AttributeLibCm[]): InfoItem[] =>
  attributes.map(mapAttributeLibCmToInfoItem);
