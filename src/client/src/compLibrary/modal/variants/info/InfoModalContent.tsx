import { FunctionComponent } from "react";
import { Dialog } from "@headlessui/react";
import {
  InfoModalContentContainer,
  InfoModalHeader,
  InfoModalHeaderDescription,
  InfoModalHeaderTitle,
} from "./InfoModalContent.styled";

interface Props {
  title?: string;
  description?: string;
  inset?: string;
  color?: string;
}

export const InfoModalContent: FunctionComponent<Props> = ({ title, description, inset, color, children }) => (
  <InfoModalContentContainer inset={inset} color={color}>
    <InfoModalHeader>
      {title && <Dialog.Title as={InfoModalHeaderTitle}>{title}</Dialog.Title>}
      {description && <Dialog.Description as={InfoModalHeaderDescription}>{description}</Dialog.Description>}
    </InfoModalHeader>
    {children}
  </InfoModalContentContainer>
);
