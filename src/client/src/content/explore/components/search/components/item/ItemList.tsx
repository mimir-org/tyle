import { AnimatePresence } from "framer-motion";
import { ReactNode } from "react";
import { useTheme } from "styled-components";
import { MotionItemListContainer } from "./ItemList.styled";

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
