import * as AlertDialogPrimitive from "@radix-ui/react-alert-dialog";
import Box from "components/Box";
import Button from "components/Button";
import ConditionalWrapper from "components/ConditionalWrapper";
import Text from "components/Text";
import VisuallyHidden from "components/VisuallyHidden";
import { ButtonHTMLAttributes, PropsWithChildren, ReactElement, ReactNode } from "react";
import { useTheme } from "styled-components";
import { AlertDialogContent, AlertDialogContentProps, AlertDialogOverlay } from "./AlertDialog.styled";

type AlertDialogProps = Pick<AlertDialogPrimitive.AlertDialogProps, "open" | "onOpenChange"> &
  AlertDialogContentProps & {
    content?: ReactNode;
    actions: AlertDialogActionItem[];
    cancelAction?: AlertDialogCancelItem;
    title: string;
    description?: string;
    hideTitle?: boolean;
    hideDescription?: boolean;
  };

/**
 * Component that interrupts the user with important content and expects a response.
 * Can operate in both a controlled and uncontrolled mode by utilizing open and onOpenChange properties.
 *
 * See documentation link below for details.
 * @see https://www.radix-ui.com/docs/primitives/components/alert-dialog
 *
 * @param children component that triggers dialog visibility
 * @param content shown inside the dialog itself
 * @param open property for overriding the open state of the dialog
 * @param onOpenChange event handler called when the open state of the dialog changes
 * @param actions what actions the user can take
 * @param cancelAction property for overriding the cancel action's text and click handler
 * @param title required title of dialog (can be hidden visually with hideTitle prop)
 * @param description optional description of dialog
 * @param hideTitle hides the title from view while remaining readable by screen-readers
 * @param hideDescription hides the description from view while remaining readable by screen-readers
 * @param delegated receives sizing and flexbox props for overriding default styles
 * @constructor
 */
const AlertDialog = ({
  children,
  content,
  open,
  onOpenChange,
  actions,
  cancelAction,
  title,
  hideTitle,
  description,
  hideDescription,
  ...delegated
}: PropsWithChildren<AlertDialogProps>) => {
  const theme = useTheme();

  return (
    <AlertDialogPrimitive.Root open={open} onOpenChange={onOpenChange}>
      <AlertDialogPrimitive.Trigger asChild>{children}</AlertDialogPrimitive.Trigger>
      <AlertDialogPrimitive.Portal>
        <AlertDialogPrimitive.Overlay asChild>
          <AlertDialogOverlay {...theme.tyle.animation.fade} />
        </AlertDialogPrimitive.Overlay>
        <AlertDialogPrimitive.Content asChild>
          <AlertDialogContent {...theme.tyle.animation.fade} {...delegated}>
            <Box display={"flex"} flexDirection={"column"} gap={theme.tyle.spacing.xl} maxWidth={"50ch"}>
              <AlertDialogTitle hide={hideTitle}>{title}</AlertDialogTitle>
              {description && <AlertDialogDescription hide={hideDescription}>{description}</AlertDialogDescription>}
            </Box>
            {content}
            <Box
              display={"flex"}
              flexWrap={"wrap"}
              justifyContent={"space-between"}
              gap={theme.tyle.spacing.base}
              minWidth={"236px"}
            >
              <AlertDialogCancel name={cancelAction?.name} onAction={cancelAction?.onAction} />
              {actions?.map((a) => <AlertDialogAction key={a.name} {...a} />)}
            </Box>
          </AlertDialogContent>
        </AlertDialogPrimitive.Content>
      </AlertDialogPrimitive.Portal>
    </AlertDialogPrimitive.Root>
  );
};

export default AlertDialog;

export type AlertDialogActionItem = Pick<ButtonHTMLAttributes<HTMLButtonElement>, "form" | "type"> & {
  name: string;
  onAction?: () => void;
};

export const AlertDialogAction = ({ name, onAction, ...delegated }: AlertDialogActionItem) => (
  <AlertDialogPrimitive.Action asChild>
    <Button variant={"filled"} onClick={onAction} {...delegated}>
      {name}
    </Button>
  </AlertDialogPrimitive.Action>
);

export interface AlertDialogCancelItem {
  name?: string;
  onAction?: () => void;
}

export const AlertDialogCancel = ({ name, onAction }: AlertDialogCancelItem) => (
  <AlertDialogPrimitive.Cancel asChild>
    <Button variant={"outlined"} onClick={onAction}>
      {name ?? "Cancel"}
    </Button>
  </AlertDialogPrimitive.Cancel>
);

interface DialogDescriptionProps {
  children: ReactNode;
  hide?: boolean;
}

const WrappedComponent = (c: ReactElement) => <VisuallyHidden asChild>{c}</VisuallyHidden>;

export const AlertDialogDescription = ({ children, hide }: DialogDescriptionProps) => {
  const theme = useTheme();

  return (
    <ConditionalWrapper condition={hide} wrapper={WrappedComponent}>
      <AlertDialogPrimitive.Description asChild>
        <Text variant={"title-medium"} textAlign={"center"} color={theme.tyle.color.surface.variant.on}>
          {children}
        </Text>
      </AlertDialogPrimitive.Description>
    </ConditionalWrapper>
  );
};

interface DialogTitleProps {
  children: ReactNode;
  hide?: boolean;
}

export const AlertDialogTitle = ({ children, hide }: DialogTitleProps) => (
  <ConditionalWrapper condition={hide} wrapper={(c) => WrappedComponent(c)}>
    <AlertDialogPrimitive.Title asChild>
      <Text variant={"title-large"} textAlign={"center"}>
        {children}
      </Text>
    </AlertDialogPrimitive.Title>
  </ConditionalWrapper>
);
