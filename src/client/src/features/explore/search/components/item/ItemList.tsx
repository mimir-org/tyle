import { MotionItemListContainer } from "features/explore/search/components/item/ItemList.styled";
import { AnimatePresence } from "framer-motion";
import { ReactNode } from "react";
import { useTheme } from "styled-components";

export interface ItemListProps {
  children: ReactNode;
}

/**
 * Component which wraps its children creating a faded scrollable list.
 *
 * @param children
 * @constructor
 */
export const ItemList = ({ children }: ItemListProps) => {
  const theme = useTheme();

  return (
    <MotionItemListContainer {...theme.tyle.animation.fade}>
      <AnimatePresence>{children}</AnimatePresence>
    </MotionItemListContainer>
  );
};
