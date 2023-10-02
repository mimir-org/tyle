import { AttributeGroupLibCm } from "@mimirorg/typelibrary-types";
import { InfoItem } from "common/types/infoItem";
import { Text } from "@mimirorg/component-library";

export const mapAttributeGroupLibCmToInfoItem = (attributeGroup: AttributeGroupLibCm): InfoItem => {
  const attributes = attributeGroup.attributes.map((x) => x.name);
  const i = attributes.map((item, index) => (index ? ", " : "") + item);

  const infoItem = {
    id: attributeGroup.id,
    name: attributeGroup.name,
    descriptors: {
      Attributes: (
        <Text as={"a"} target={"_blank"} rel={"noopener noreferrer"} variant={"body-small"} color={"inherit"}>
          {i}
        </Text>
      ),
    },
  };

  return {
    ...infoItem,
    descriptors: {
      ...infoItem.descriptors,
    },
  };

  return infoItem;
};

export const mapAttributeGroupLibCmsToInfoItems = (attributes: AttributeGroupLibCm[]): InfoItem[] =>
  attributes.map(mapAttributeGroupLibCmToInfoItem);
