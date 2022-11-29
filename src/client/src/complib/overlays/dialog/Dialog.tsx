import * as DialogPrimitive from "@radix-ui/react-dialog";
import { Box } from "complib/layouts";
import { DialogDescription } from "complib/overlays/dialog/components/DialogDescription";
import { DialogExit } from "complib/overlays/dialog/components/DialogExit";
import { DialogTitle } from "complib/overlays/dialog/components/DialogTitle";
import { DialogContent, DialogContentProps, DialogOverlay } from "complib/overlays/dialog/Dialog.styled";
import { ReactNode } from "react";
import { useTheme } from "styled-components";

export type DialogProps = Pick<DialogPrimitive.DialogProps, "open" | "onOpenChange"> &
  DialogContentProps & {
    children?: ReactNode;
    content: ReactNode;
    title: string;
    description?: string;
    hideTitle?: boolean;
    hideDescription?: boolean;
    closeText?: string;
  };

/**
 * Component which is overlaid the primary window, rendering the content underneath inert.
 * Can operate in both a controlled and uncontrolled mode by utilizing open and onOpenChange properties.
 *
 * See documentation link below for details.
 * @see https://www.radix-ui.com/docs/primitives/components/dialog
 *
 * @param children component that triggers dialog visibility
 * @param content shown inside the dialog itself
 * @param open property for overriding the open state of the dialog
 * @param onOpenChange event handler called when the open state of the dialog changes
 * @param title required title of dialog (can be hidden visually with hideTitle prop)
 * @param description optional description of dialog
 * @param hideTitle hides the title from view while remaining readable by screen-readers
 * @param hideDescription hides the description from view while remaining readable by screen-readers
 * @param closeText property for overriding the default text for closing the dialog (screen-readers)
 * @param delegated receives sizing and flexbox props for overriding default styles
 * @constructor
 */
export const Dialog = ({
  children,
  content,
  open,
  onOpenChange,
  title,
  hideTitle,
  description,
  hideDescription,
  closeText,
  ...delegated
}: DialogProps) => {
  const theme = useTheme();

  return (
    <DialogPrimitive.Root open={open} onOpenChange={onOpenChange}>
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
            <DialogExit closeText={closeText} />
          </DialogContent>
        </DialogPrimitive.Content>
      </DialogPrimitive.Portal>
    </DialogPrimitive.Root>
  );
};
