import { ReactNode } from "react";
import { useTheme } from "styled-components";
import { Box, Flexbox } from "../../../../../../complib/layouts";
import { MotionCard } from "../../../../../../complib/surfaces";

export interface ItemProps {
  isSelected?: boolean;
  preview: ReactNode;
  description: ReactNode;
  actions: ReactNode;
}

/**
 * Component which presents information about an item.
 * It has 3 slots (preview, description, actions) which you can populate with your own components.
 *
 * @param isSelected controls the selected style of the item
 * @param preview slot for visual preview of the item
 * @param description slot for description of the item
 * @param actions slot for actions you can do with the item (usually buttons)
 * @constructor
 */
export const Item = ({ isSelected, preview, description, actions }: ItemProps) => {
  const theme = useTheme();

  return (
    <MotionCard
      layout
      variant={isSelected ? "outlined" : "elevated"}
      {...theme.tyle.animation.fade}
      {...theme.tyle.animation.selectHover}
    >
      <Flexbox justifyContent={"space-between"} alignItems={"center"} flexWrap={"wrap"} gap={theme.tyle.spacing.medium}>
        {preview}
        {description}
        <Box display={"flex"} gap={theme.tyle.spacing.small} ml={"auto"}>
          {actions}
        </Box>
      </Flexbox>
    </MotionCard>
  );
};
