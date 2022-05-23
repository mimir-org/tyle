import { Divider, Popover } from "../../../../../../complib/data-display";
import { useTheme } from "styled-components";
import { Box, Flexbox } from "../../../../../../complib/layouts";
import { AttributeButton, AttributeButtonProps } from "./AttributeButton";
import { AttributeItem } from "../../../../types/AttributeItem";
import { Text } from "../../../../../../complib/text";
import { TextResources } from "../../../../../../assets/text";

type AttributeSingleProps = AttributeButtonProps & AttributeItem;

/**
 * Component which shows a single attribute for a given entity in addition to its description in a popover.
 *
 * @param name of attribute
 * @param color representing attribute
 * @param traits various qualities/traits that the attribute has
 * @param value associated with attribute e.g. percentage, dimension etc.
 * @param delegated receives all properties which AttributeButtonProps define
 * @constructor
 */
export const AttributeSingle = ({ name, color, traits, value, ...delegated }: AttributeSingleProps) => {
  const buttonName = name.split(",")[0];

  return (
    <Popover content={<AttributeDescription name={name} color={color} traits={traits} value={value} />}>
      <AttributeButton color={color} {...delegated}>
        {buttonName}
      </AttributeButton>
    </Popover>
  );
};

export const AttributeDescription = ({ name, color, traits, value }: AttributeItem) => {
  const theme = useTheme();

  return (
    <Box
      position={"relative"}
      display={"flex"}
      flexDirection={"column"}
      width={"175px"}
      minHeight={"90px"}
      gap={theme.tyle.spacing.small}
    >
      <Box position={"absolute"} top={0} right={0} width={"15px"} height={"15px"} bgColor={color} />
      <Text variant={"title-medium"} useEllipsis ellipsisMaxLines={3} pr={theme.tyle.spacing.large}>
        {name}
      </Text>

      {traits &&
        Object.keys(traits).map((k, i) => (
          <Flexbox key={i} gap={theme.tyle.spacing.medium} justifyContent={"space-between"}>
            <Text variant={"body-medium"} textTransform={"capitalize"}>
              {k}:
            </Text>
            <Text variant={"label-large"} textTransform={"capitalize"}>
              {traits[k]}
            </Text>
          </Flexbox>
        ))}

      <Divider />

      <Flexbox gap={theme.tyle.spacing.xs} justifyContent={"space-between"}>
        <Text>{TextResources.ATTRIBUTE_VALUE}:</Text>
        <Text>{value}</Text>
      </Flexbox>
    </Box>
  );
};
