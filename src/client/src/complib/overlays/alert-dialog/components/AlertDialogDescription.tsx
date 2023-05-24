import * as AlertDialogPrimitive from "@radix-ui/react-alert-dialog";
import { VisuallyHidden } from "complib/accessibility";
import { Text } from "complib/text";
import { ConditionalWrapper } from "complib/utils";
import { ReactElement, ReactNode } from "react";
import { useTheme } from "styled-components";

interface DialogDescriptionProps {
  children: ReactNode;
  hide?: boolean;
}

export const AlertDialogDescription = ({ children, hide }: DialogDescriptionProps) => {
  const theme = useTheme();

  const WrappedComponent = (c: ReactElement) => <VisuallyHidden asChild>{c}</VisuallyHidden>;

  return (
    <ConditionalWrapper condition={hide} wrapper={WrappedComponent}>
      <AlertDialogPrimitive.Description asChild>
        <Text variant={"title-medium"} textAlign={"center"} color={theme.tyle.color.sys.surface.variant.on}>
          {children}
        </Text>
      </AlertDialogPrimitive.Description>
    </ConditionalWrapper>
  );
};
