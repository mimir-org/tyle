import { useTheme } from "styled-components";
import { Box, Flexbox } from "../../../../../../complib/layouts";
import { Text } from "../../../../../../complib/text";
import { AttributeItem } from "../../../../types/AttributeItem";

/**
 * Component summarizes information about a given attribute.
 * This component is most often shown inside either a tooltip or a popover.
 *
 * @param name
 * @param color
 * @param traits
 * @constructor
 */
export const AttributeDescription = ({ name, color, traits }: Omit<AttributeItem, "id">) => {
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
    </Box>
  );
};
