import { Dialog } from "@headlessui/react";
import { FunctionComponent } from "react";
import { ModalContent } from "./components/ModalContent";
import { ModalContainer, ModalLayoutContainer, ModalOverlay } from "./Modal.styled";

interface Props {
  onExit: () => void;
  isOpen: boolean;
  isBlurred?: boolean;
  isFaded?: boolean;
  title?: string;
  description?: string;
}

/**
 * Component for a generic modal.
 * @param interface
 * @returns a centered modal a background overlay.
 */
export const Modal: FunctionComponent<Props> = ({
  onExit,
  isOpen,
  isBlurred,
  isFaded,
  children,
  title,
  description,
}) => (
  <Dialog open={isOpen} onClose={onExit} as={ModalContainer}>
    <ModalLayoutContainer>
      <Dialog.Overlay as={ModalOverlay} isBlurred={isBlurred} isFaded={isFaded} />
      <ModalContent onExit={onExit} title={title} description={description}>
        {children}
      </ModalContent>
    </ModalLayoutContainer>
  </Dialog>
);
