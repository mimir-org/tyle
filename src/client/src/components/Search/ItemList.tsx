import { AnimatePresence } from "framer-motion";
import { ReactNode } from "react";
import { useTheme } from "styled-components";
import MotionItemListContainer from "./ItemList.styled";

export interface ItemListProps {
  children: ReactNode;
}

/**
 * Component which wraps its children creating a faded scrollable list.
 *
 * @param children
 * @constructor
 */
const ItemList = ({ children }: ItemListProps) => {
  const theme = useTheme();

  return (
    <MotionItemListContainer {...theme.mimirorg.animation.fade}>
      <AnimatePresence>{children}</AnimatePresence>
    </MotionItemListContainer>
  );
};

export default ItemList;
