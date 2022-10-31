import * as DialogPrimitive from "@radix-ui/react-dialog";
import { VisuallyHidden } from "complib/accessibility";
import { Text } from "complib/text";
import { ConditionalWrapper } from "complib/utils";
import { ReactNode } from "react";

interface DialogTitleProps {
  children: ReactNode;
  hide?: boolean;
}

export const DialogTitle = ({ children, hide }: DialogTitleProps) => (
  <ConditionalWrapper condition={hide} wrapper={(c) => <VisuallyHidden asChild>{c}</VisuallyHidden>}>
    <DialogPrimitive.Title asChild>
      <Text variant={"title-large"} textAlign={"center"}>
        {children}
      </Text>
    </DialogPrimitive.Title>
  </ConditionalWrapper>
);
