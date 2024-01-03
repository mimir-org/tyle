import { MotionCard } from "@mimirorg/component-library";
import Box from "components/Box";
import { ReactNode } from "react";
import { useTheme } from "styled-components";
import ItemActionContainer from "./Item.styled";

export interface ItemProps {
  isSelected?: boolean;
  preview: ReactNode;
  description: ReactNode;
  actions: ReactNode;
  onClick?: () => void;
}

/**
 * Component which presents information about an item.
 * It has 3 slots (preview, description, actions) which you can populate with your own components.
 *
 * @param isSelected controls the selected style of the item
 * @param preview slot for visual preview of the item
 * @param description slot for description of the item
 * @param actions slot for actions you can do with the item (usually buttons)
 * @param onClick callback for when the item is clicked
 * @constructor
 */
const Item = ({ isSelected, preview, description, actions, onClick }: ItemProps) => {
  const theme = useTheme();

  return (
    <MotionCard
      onClick={onClick}
      layout={"position"}
      variant={isSelected ? "selected" : "filled"}
      {...theme.tyle.animation.selectHover}
    >
      <Box
        position={"relative"}
        display={"flex"}
        justifyContent={"space-between"}
        alignItems={"start"}
        flexWrap={"wrap"}
        gap={theme.tyle.spacing.xxxl}
      >
        {preview}
        {description}
        <ItemActionContainer>{actions}</ItemActionContainer>
      </Box>
    </MotionCard>
  );
};

export default Item;
