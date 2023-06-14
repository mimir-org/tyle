import * as DialogPrimitive from "@radix-ui/react-dialog";
import { VisuallyHidden } from "complib/accessibility";
import { Text } from "complib/text";
import { ConditionalWrapper } from "complib/utils";
import { ReactNode } from "react";
import { useTheme } from "styled-components";

interface DialogDescriptionProps {
  children: ReactNode;
  hide?: boolean;
}

const WrappedComponent = (c: ReactNode) => <VisuallyHidden asChild>{c}</VisuallyHidden>;
export const DialogDescription = ({ children, hide }: DialogDescriptionProps) => {
  const theme = useTheme();

  return (
    <ConditionalWrapper condition={hide} wrapper={(c) => WrappedComponent(c)}>
      <DialogPrimitive.Description asChild>
        <Text variant={"title-medium"} textAlign={"center"} color={theme.tyle.color.sys.surface.variant.on}>
          {children}
        </Text>
      </DialogPrimitive.Description>
    </ConditionalWrapper>
  );
};
