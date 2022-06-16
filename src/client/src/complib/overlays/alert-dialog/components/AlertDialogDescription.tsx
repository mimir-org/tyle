import * as AlertDialogPrimitive from "@radix-ui/react-alert-dialog";
import { ReactNode } from "react";
import { useTheme } from "styled-components";
import { ConditionalWrapper } from "../../../utils";
import { VisuallyHidden } from "../../../accessibility";
import { Text } from "../../../text";

interface DialogDescriptionProps {
  children: ReactNode;
  hide?: boolean;
}

export const AlertDialogDescription = ({ children, hide }: DialogDescriptionProps) => {
  const theme = useTheme();

  return (
    <ConditionalWrapper condition={hide} wrapper={(c) => <VisuallyHidden asChild>{c}</VisuallyHidden>}>
      <AlertDialogPrimitive.Description asChild>
        <Text variant={"label-large"} color={theme.tyle.color.sys.surface.variant.on}>
          {children}
        </Text>
      </AlertDialogPrimitive.Description>
    </ConditionalWrapper>
  );
};
