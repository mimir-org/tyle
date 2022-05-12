import { Box } from "../../../../../../complib/layouts";
import { Button } from "../../../../../../complib/buttons";
import { useTheme } from "styled-components";
import { Duplicate, PencilAlt, Trash } from "@styled-icons/heroicons-outline";
import { TextResources } from "../../../../../../assets/text";

/**
 * [POC]
 * Components which displays the various actions one can perform on an item.
 *
 * @constructor
 */
export const ItemActions = () => {
  const theme = useTheme();

  return (
    <Box display={"flex"} flexWrap={"wrap"} gap={theme.tyle.spacing.small} mr={theme.tyle.spacing.small} ml={"auto"}>
      <Button disabled variant={"filled"} icon={<Duplicate />} iconOnly>
        {TextResources.ITEM_CLONE}
      </Button>
      <Button disabled variant={"filled"} icon={<PencilAlt />} iconOnly>
        {TextResources.ITEM_EDIT}
      </Button>
      <Button disabled variant={"outlined"} icon={<Trash />} iconOnly>
        {TextResources.ITEM_DELETE}
      </Button>
    </Box>
  );
};
