import { AnimatePresence } from "framer-motion";
import { ItemListContainer } from "./ItemList.styled";
import { ReactNode } from "react";

interface SearchListProps {
  children: ReactNode;
}

/**
 * Component which wraps its children creating a faded scrollable list.
 *
 * @param children
 * @constructor
 */
export const ItemList = ({ children }: SearchListProps) => (
  <ItemListContainer>
    <AnimatePresence>{children}</AnimatePresence>
  </ItemListContainer>
);
