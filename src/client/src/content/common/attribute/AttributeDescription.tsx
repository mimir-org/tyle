import { useTheme } from "styled-components";
import { Button } from "../../../complib/buttons";
import { Divider } from "../../../complib/data-display";
import { Box, Flexbox } from "../../../complib/layouts";
import { Text } from "../../../complib/text";
import { Actionable } from "../../../complib/types";
import { AttributeItem } from "../../types/AttributeItem";

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
    <Box as={"section"} display={"flex"} flexDirection={"column"} width={"118px"} gap={theme.tyle.spacing.l}>
      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.base}>
        <Flexbox justifyContent={"space-between"} alignItems={"center"}>
          <Text variant={"title-small"}>{name}</Text>
          {actionable && onAction && (
            <Button
              variant={"filled"}
              color={theme.tyle.color.sys.surface.on}
              onClick={onAction}
              icon={actionIcon}
              iconOnly
            >
              {actionText}
            </Button>
          )}
        </Flexbox>
        <Divider />
      </Flexbox>

      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.base}>
        {traits &&
          Object.keys(traits).map((k, i) => (
            <Text key={i} variant={"body-small"} textTransform={"capitalize"}>
              <Box as={"span"} color={theme.tyle.color.sys.secondary.base}>
                {k}:{" "}
              </Box>
              {traits[k]}
            </Text>
          ))}
      </Flexbox>
    </Box>
  );
};
