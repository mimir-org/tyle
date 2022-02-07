import { FunctionComponent } from "react";
import { Dialog } from "@headlessui/react";
import { WarningIcon } from "../../../../assets/icons/common";
import {
  NotificationModalContentContainer,
  NotificationModalDescription,
  NotificationWarningIcon,
} from "./NotificationModalContent.styled";

interface Props {
  description?: string;
  isWarning?: boolean;
}

export const NotificationModalContent: FunctionComponent<Props> = ({ description, isWarning, children }) => (
  <NotificationModalContentContainer isWarning={isWarning}>
    {isWarning && <NotificationWarningIcon size={15} src={WarningIcon} alt="warning triangle" />}
    {description && <Dialog.Description as={NotificationModalDescription}>{description}</Dialog.Description>}
    {children}
  </NotificationModalContentContainer>
);
