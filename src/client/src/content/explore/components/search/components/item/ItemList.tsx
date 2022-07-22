import { AnimatePresence } from "framer-motion";
import { ReactNode } from "react";
import { ItemListContainer } from "./ItemList.styled";

export interface ItemListProps {
  children: ReactNode;
}

/**
 * Component which wraps its children creating a faded scrollable list.
 *
 * @param children
 * @constructor
 */
export const ItemList = ({ children }: ItemListProps) => (
  <ItemListContainer>
    <AnimatePresence>{children}</AnimatePresence>
  </ItemListContainer>
);
