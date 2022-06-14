import * as DialogPrimitive from "@radix-ui/react-dialog";
import { ReactNode } from "react";
import { useTheme } from "styled-components";
import { ConditionalWrapper } from "../../../utils";
import { VisuallyHidden } from "../../../accessibility";
import { Text } from "../../../text";

interface DialogDescriptionProps {
  children: ReactNode;
  hide?: boolean;
}

export const DialogDescription = ({ children, hide }: DialogDescriptionProps) => {
  const theme = useTheme();

  return (
    <ConditionalWrapper condition={hide} wrapper={(c) => <VisuallyHidden asChild>{c}</VisuallyHidden>}>
      <DialogPrimitive.Description asChild>
        <Text variant={"label-large"} color={theme.tyle.color.surface.variant.on}>
          {children}
        </Text>
      </DialogPrimitive.Description>
    </ConditionalWrapper>
  );
};
