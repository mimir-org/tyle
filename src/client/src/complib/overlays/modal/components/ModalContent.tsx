import { FunctionComponent } from "react";
import { Dialog } from "@headlessui/react";
import { ModalContentContainer, ModalHeaderDescription, ModalHeaderTitle } from "./ModalContent.styled";
import { Box } from "../../../layouts";
import ExitButton from "./ExitButton";

interface Props {
  onExit: () => void;
  title?: string;
  description?: string;
}

export const ModalContent: FunctionComponent<Props> = ({ onExit, title, description, children }) => (
  <ModalContentContainer>
    <ExitButton onClick={onExit} />
    <Box as={"header"}>
      {title && <Dialog.Title as={ModalHeaderTitle}>{title}</Dialog.Title>}
      {description && <Dialog.Description as={ModalHeaderDescription}>{description}</Dialog.Description>}
    </Box>
    {children}
  </ModalContentContainer>
);
