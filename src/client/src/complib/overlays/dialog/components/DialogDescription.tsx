import * as DialogPrimitive from "@radix-ui/react-dialog";
import { ReactNode } from "react";
import { useTheme } from "styled-components";
import { VisuallyHidden } from "../../../accessibility";
import { Text } from "../../../text";
import { ConditionalWrapper } from "../../../utils";

interface DialogDescriptionProps {
  children: ReactNode;
  hide?: boolean;
}

export const DialogDescription = ({ children, hide }: DialogDescriptionProps) => {
  const theme = useTheme();

  return (
    <ConditionalWrapper condition={hide} wrapper={(c) => <VisuallyHidden asChild>{c}</VisuallyHidden>}>
      <DialogPrimitive.Description asChild>
        <Text variant={"title-medium"} textAlign={"center"} color={theme.tyle.color.sys.surface.variant.on}>
          {children}
        </Text>
      </DialogPrimitive.Description>
    </ConditionalWrapper>
  );
};
