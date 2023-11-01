import { InfoItem } from "common/types/infoItem";
import { Text } from "@mimirorg/component-library";
import { AttributeView } from "common/types/attributes/attributeView";

export const mapAttributeViewToInfoItem = (attribute: AttributeView): InfoItem => {
  const infoItem = {
    id: attribute.id,
    name: attribute.name,
    descriptors: {
      Predicate: (
        <Text
          as={"a"}
          href={attribute.predicate?.iri}
          target={"_blank"}
          rel={"noopener noreferrer"}
          variant={"body-small"}
          color={"inherit"}
        >
          {attribute.predicate?.iri}
        </Text>
      ),
    },
  };

  const attributeHasUnits = attribute.units.length > 0;
  if (attributeHasUnits) {
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

export const mapAttributeViewsToInfoItems = (attributes: AttributeView[]): InfoItem[] =>
  attributes.map(mapAttributeViewToInfoItem);
