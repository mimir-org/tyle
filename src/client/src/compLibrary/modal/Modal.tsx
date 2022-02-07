import { Dialog } from "@headlessui/react";
import { ModalContainer, ModalContentContainer, ModalOverlay } from "./Modal.styled";
import { FunctionComponent } from "react";

interface Props {
  onExit: () => void;
  isOpen: boolean;
  isBlurred?: boolean;
  isFaded?: boolean;
}

/**
 * Component for a generic modal in Mimir.
 * @param interface
 * @returns a centered modal a background overlay.
 */
export const Modal: FunctionComponent<Props> = ({ onExit, isOpen, isBlurred, isFaded, children }) => (
  <Dialog open={isOpen} onClose={onExit} as={ModalContainer}>
    <ModalContentContainer>
      <Dialog.Overlay as={ModalOverlay} isBlurred={isBlurred} isFaded={isFaded} />
      {children}
    </ModalContentContainer>
  </Dialog>
);
