import * as DialogPrimitive from "@radix-ui/react-dialog";
import { ReactNode } from "react";
import { ConditionalWrapper } from "../../../utils";
import { VisuallyHidden } from "../../../accessibility";
import { Text } from "../../../text";

interface DialogTitleProps {
  children: ReactNode;
  hide?: boolean;
}

export const DialogTitle = ({ children, hide }: DialogTitleProps) => (
  <ConditionalWrapper condition={hide} wrapper={(c) => <VisuallyHidden asChild>{c}</VisuallyHidden>}>
    <DialogPrimitive.Title asChild>
      <Text variant={"title-medium"}>{children}</Text>
    </DialogPrimitive.Title>
  </ConditionalWrapper>
);
