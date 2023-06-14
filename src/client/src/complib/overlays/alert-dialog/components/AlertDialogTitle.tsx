import * as AlertDialogPrimitive from "@radix-ui/react-alert-dialog";
import { VisuallyHidden } from "complib/accessibility";
import { Text } from "complib/text";
import { ConditionalWrapper } from "complib/utils";
import { ReactNode } from "react";

interface DialogTitleProps {
  children: ReactNode;
  hide?: boolean;
}

const WrappedComponent = (c: ReactNode) => <VisuallyHidden asChild>{c}</VisuallyHidden>;

export const AlertDialogTitle = ({ children, hide }: DialogTitleProps) => (
  <ConditionalWrapper condition={hide} wrapper={(c) => WrappedComponent(c)}>
    <AlertDialogPrimitive.Title asChild>
      <Text variant={"title-large"} textAlign={"center"}>
        {children}
      </Text>
    </AlertDialogPrimitive.Title>
  </ConditionalWrapper>
);
