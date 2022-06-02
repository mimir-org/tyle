import { useTheme } from "styled-components";
import { Box, Flexbox } from "../../../../../../complib/layouts";
import { Text } from "../../../../../../complib/text";
import { AttributeItem } from "../../../../types/AttributeItem";
import { Button } from "../../../../../../complib/buttons";
import { Actionable } from "../../../../../../types";

export type AttributeDescriptionProps = Omit<AttributeItem, "id"> & Partial<Actionable>;

/**
 * Component summarizes information about a given attribute.
 * This component is most often shown inside either a tooltip or a popover.
 *
 * @param name
 * @param traits
 * @param actionable
 * @param actionIcon
 * @param actionText
 * @param onAction
 * @constructor
 */
export const AttributeDescription = ({
  name,
  traits,
  actionable,
  actionIcon,
  actionText,
  onAction,
}: AttributeDescriptionProps) => {
  const theme = useTheme();

  return (
    <Box
      as={"section"}
      position={"relative"}
      display={"flex"}
      flexDirection={"column"}
      width={"150px"}
      gap={theme.tyle.spacing.medium}
    >
      <Flexbox justifyContent={"space-between"} alignItems={"start"} gap={theme.tyle.spacing.xs}>
        <Text variant={"title-medium"}>{name}</Text>
        {actionable && onAction && (
          <Button variant={"filled"} icon={actionIcon} iconOnly onClick={onAction} color={theme.tyle.color.surface.on}>
            {actionText}
          </Button>
        )}
      </Flexbox>

      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.xxs}>
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
      </Flexbox>
    </Box>
  );
};
