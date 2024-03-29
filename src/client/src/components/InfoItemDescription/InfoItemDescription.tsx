import Box from "components/Box";
import Button from "components/Button";
import Divider from "components/Divider";
import Flexbox from "components/Flexbox";
import Text from "components/Text";
import { useTheme } from "styled-components";
import { Actionable } from "types/actionable";
import { InfoItem } from "types/infoItem";

export type InfoItemDescriptionProps = Omit<InfoItem, "id"> & Partial<Actionable>;

/**
 * Component summarizes information about a given generic item.
 * This component is most often shown inside either a tooltip or a popover.
 *
 * @param name of item
 * @param descriptors various qualities/traits that the item has
 * @param actionable enables action button in popover
 * @param actionIcon icon disabled inside action button
 * @param actionText action button text (hidden if icon is supplied)
 * @param onAction called when clicking action button
 * @constructor
 */
const InfoItemDescription = ({
  name,
  descriptors,
  actionable,
  actionIcon,
  actionText,
  onAction,
}: InfoItemDescriptionProps) => {
  const theme = useTheme();

  return (
    <Box as={"section"} display={"flex"} flexDirection={"column"} width={"118px"} gap={theme.tyle.spacing.l}>
      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.base}>
        <Flexbox gap={theme.tyle.spacing.s} justifyContent={"space-between"} alignItems={"center"}>
          <Text variant={"title-small"}>{name}</Text>
          {actionable && onAction && (
            <Button
              variant={"filled"}
              color={theme.tyle.color.surface.on}
              onClick={onAction}
              icon={actionIcon}
              iconOnly
              flexShrink={"0"}
            >
              {actionText}
            </Button>
          )}
        </Flexbox>
        <Divider />
      </Flexbox>

      <Box
        display={"flex"}
        flexDirection={"column"}
        gap={theme.tyle.spacing.base}
        maxHeight={"250px"}
        overflow={"auto"}
      >
        {descriptors &&
          Object.keys(descriptors).map((k, i) => (
            <Text key={k + i} variant={"body-small"} color={theme.tyle.color.primary.on}>
              <Text as={"span"} color={theme.tyle.color.secondary.base} textTransform={"capitalize"}>
                {k}:{" "}
              </Text>
              {descriptors[k]}
            </Text>
          ))}
      </Box>
    </Box>
  );
};

export default InfoItemDescription;
