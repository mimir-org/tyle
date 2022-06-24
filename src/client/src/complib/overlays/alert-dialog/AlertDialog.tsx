import * as AlertDialogPrimitive from "@radix-ui/react-alert-dialog";
import { PropsWithChildren, ReactNode } from "react";
import { useTheme } from "styled-components";
import { Box } from "../../layouts";
import { AlertDialogContent, AlertDialogContentProps, AlertDialogOverlay } from "./AlertDialog.styled";
import { AlertDialogAction, AlertDialogActionItem } from "./components/AlertDialogAction";
import { AlertDialogCancel } from "./components/AlertDialogCancel";
import { AlertDialogDescription } from "./components/AlertDialogDescription";
import { AlertDialogTitle } from "./components/AlertDialogTitle";

type AlertDialogProps = AlertDialogContentProps & {
  content?: ReactNode;
  actions: AlertDialogActionItem[];
  title: string;
  description?: string;
  hideTitle?: boolean;
  hideDescription?: boolean;
};

/**
 * Component that interrupts the user with important content and expects a response
 *
 * @param children component that triggers dialog visibility
 * @param content shown inside the dialog itself
 * @param actions what actions the user can take
 * @param title required title of dialog (can be hidden visually with hideTitle prop)
 * @param description optional description of dialog
 * @param hideTitle hides the title from view while remaining readable by screen-readers
 * @param hideDescription hides the description from view while remaining readable by screen-readers
 * @param delegated receives sizing and flexbox props for overriding default styles
 * @constructor
 */
export const AlertDialog = ({
  children,
  content,
  actions,
  title,
  hideTitle,
  description,
  hideDescription,
  ...delegated
}: PropsWithChildren<AlertDialogProps>) => {
  const theme = useTheme();

  return (
    <AlertDialogPrimitive.Root>
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
              <AlertDialogCancel />
              {actions?.map((a) => (
                <AlertDialogAction key={a.name} {...a} />
              ))}
            </Box>
          </AlertDialogContent>
        </AlertDialogPrimitive.Content>
      </AlertDialogPrimitive.Portal>
    </AlertDialogPrimitive.Root>
  );
};
