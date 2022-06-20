import * as DialogPrimitive from "@radix-ui/react-dialog";
import { PropsWithChildren, ReactNode } from "react";
import { useTheme } from "styled-components";
import { Flexbox } from "../../layouts";
import { DialogDescription } from "./components/DialogDescription";
import { DialogExit } from "./components/DialogExit";
import { DialogTitle } from "./components/DialogTitle";
import { DialogContent, DialogOverlay } from "./Dialog.styled";

interface Props {
  content: ReactNode;
  title: string;
  description?: string;
  hideTitle?: boolean;
  hideDescription?: boolean;
}

/**
 * Component which is overlaid the primary window, rendering the content underneath inert.
 *
 * @param children component that triggers dialog visibility
 * @param content shown inside the dialog itself
 * @param title required title of dialog (can be hidden visually with hideTitle prop)
 * @param description optional description of dialog
 * @param hideTitle hides the title from view while remaining readable by screen-readers
 * @param hideDescription hides the description from view while remaining readable by screen-readers
 * @constructor
 */
export const Dialog = ({
  children,
  content,
  title,
  hideTitle,
  description,
  hideDescription,
}: PropsWithChildren<Props>) => {
  const theme = useTheme();

  return (
    <DialogPrimitive.Root>
      <DialogPrimitive.Trigger asChild>{children}</DialogPrimitive.Trigger>
      <DialogPrimitive.Portal>
        <DialogPrimitive.Overlay asChild>
          <DialogOverlay {...theme.tyle.animation.fade} />
        </DialogPrimitive.Overlay>
        <DialogPrimitive.Content asChild>
          <DialogContent {...theme.tyle.animation.fade}>
            <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.xs}>
              <DialogTitle hide={hideTitle}>{title}</DialogTitle>
              {description && <DialogDescription hide={hideDescription}>{description}</DialogDescription>}
            </Flexbox>
            {content}
            <DialogExit />
          </DialogContent>
        </DialogPrimitive.Content>
      </DialogPrimitive.Portal>
    </DialogPrimitive.Root>
  );
};
