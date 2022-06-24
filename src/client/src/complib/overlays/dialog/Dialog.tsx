import * as DialogPrimitive from "@radix-ui/react-dialog";
import { ReactNode } from "react";
import { useTheme } from "styled-components";
import { Box } from "../../layouts";
import { DialogDescription } from "./components/DialogDescription";
import { DialogExit } from "./components/DialogExit";
import { DialogTitle } from "./components/DialogTitle";
import { DialogContent, DialogContentProps, DialogOverlay } from "./Dialog.styled";

export type DialogProps = DialogContentProps & {
  children?: ReactNode;
  content: ReactNode;
  title: string;
  description?: string;
  hideTitle?: boolean;
  hideDescription?: boolean;
};

/**
 * Component which is overlaid the primary window, rendering the content underneath inert.
 *
 * @param children component that triggers dialog visibility
 * @param content shown inside the dialog itself
 * @param title required title of dialog (can be hidden visually with hideTitle prop)
 * @param description optional description of dialog
 * @param hideTitle hides the title from view while remaining readable by screen-readers
 * @param hideDescription hides the description from view while remaining readable by screen-readers
 * @param delegated receives sizing and flexbox props for overriding default styles
 * @constructor
 */
export const Dialog = ({
  children,
  content,
  title,
  hideTitle,
  description,
  hideDescription,
  ...delegated
}: DialogProps) => {
  const theme = useTheme();

  return (
    <DialogPrimitive.Root>
      <DialogPrimitive.Trigger asChild>{children}</DialogPrimitive.Trigger>
      <DialogPrimitive.Portal>
        <DialogPrimitive.Overlay asChild>
          <DialogOverlay {...theme.tyle.animation.fade} />
        </DialogPrimitive.Overlay>
        <DialogPrimitive.Content asChild>
          <DialogContent {...theme.tyle.animation.fade} {...delegated}>
            <Box display={"flex"} flexDirection={"column"} gap={theme.tyle.spacing.xl} maxWidth={"50ch"}>
              <DialogTitle hide={hideTitle}>{title}</DialogTitle>
              {description && <DialogDescription hide={hideDescription}>{description}</DialogDescription>}
            </Box>
            {content}
            <DialogExit />
          </DialogContent>
        </DialogPrimitive.Content>
      </DialogPrimitive.Portal>
    </DialogPrimitive.Root>
  );
};
