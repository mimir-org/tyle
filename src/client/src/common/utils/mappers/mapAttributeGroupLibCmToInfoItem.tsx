import { AttributeGroupLibCm } from "@mimirorg/typelibrary-types";
import { InfoItem } from "common/types/infoItem";
import { Text } from "@mimirorg/component-library";

export const mapAttributeGroupLibCmToInfoItem = (attributeGroup: AttributeGroupLibCm): InfoItem => {
  const infoItem = {
    id: attributeGroup.id,
    name: attributeGroup.name,
    descriptors: {
      IRI: (
        <Text as={"a"} target={"_blank"} rel={"noopener noreferrer"} variant={"body-small"} color={"inherit"}></Text>
      ),
    },
  };

  if (true) {
    return {
      ...infoItem,
      descriptors: {
        ...infoItem.descriptors,
      },
    };
  }

  return infoItem;
};

export const mapAttributeGroupLibCmsToInfoItems = (attributes: AttributeGroupLibCm[]): InfoItem[] =>
  attributes.map(mapAttributeGroupLibCmToInfoItem);
