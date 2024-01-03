import * as DialogPrimitive from "@radix-ui/react-dialog";
import { Close } from "@styled-icons/material";
import Box from "components/Box";
import Button from "components/Button";
import ConditionalWrapper from "components/ConditionalWrapper";
import Text from "components/Text";
import VisuallyHidden from "components/VisuallyHidden";
import { forwardRef, ReactNode } from "react";
import { useTheme } from "styled-components";
import { DialogContent, DialogContentProps, DialogOverlay } from "./Dialog.styled";

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
const Dialog = forwardRef<HTMLButtonElement, DialogProps>(
  (
    {
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
    }: DialogProps,
    ref,
  ) => {
    const theme = useTheme();

    return (
      <DialogPrimitive.Root open={open} onOpenChange={onOpenChange}>
        <DialogPrimitive.Trigger asChild ref={ref}>
          {children}
        </DialogPrimitive.Trigger>

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
  },
);

Dialog.displayName = "Dialog";
Dialog.defaultProps = {};

interface DialogDescriptionProps {
  children: ReactNode;
  hide?: boolean;
}

export default Dialog;

export const DialogDescription = ({ children, hide }: DialogDescriptionProps) => {
  const theme = useTheme();

  return (
    <ConditionalWrapper condition={hide} wrapper={(c) => <VisuallyHidden asChild>{c}</VisuallyHidden>}>
      <DialogPrimitive.Description asChild>
        <Text variant={"title-medium"} textAlign={"center"} color={theme.tyle.color.surface.variant.on}>
          {children}
        </Text>
      </DialogPrimitive.Description>
    </ConditionalWrapper>
  );
};

export const DialogExit = ({ closeText }: { closeText?: string }) => {
  const theme = useTheme();
  return (
    <DialogPrimitive.Close asChild>
      <Box position={"absolute"} top={theme.tyle.spacing.xl} right={theme.tyle.spacing.xl}>
        <Button width="25px" height="25px" variant={"text"} icon={<Close />} iconOnly>
          {closeText ?? "Close dialog"}
        </Button>
      </Box>
    </DialogPrimitive.Close>
  );
};

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
