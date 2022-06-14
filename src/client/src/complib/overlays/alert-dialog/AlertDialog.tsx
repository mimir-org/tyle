import * as AlertDialogPrimitive from "@radix-ui/react-alert-dialog";
import { PropsWithChildren, ReactNode } from "react";
import { AlertDialogContent, AlertDialogOverlay } from "./AlertDialog.styled";
import { Box } from "../../layouts";
import { useTheme } from "styled-components";
import { AlertDialogTitle } from "./components/AlertDialogTitle";
import { AlertDialogDescription } from "./components/AlertDialogDescription";
import { AlertDialogCancel } from "./components/AlertDialogCancel";
import { AlertDialogAction, AlertDialogActionItem } from "./components/AlertDialogAction";

interface Props {
  content?: ReactNode;
  actions: AlertDialogActionItem[];
  title: string;
  description?: string;
  hideTitle?: boolean;
  hideDescription?: boolean;
}

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
}: PropsWithChildren<Props>) => {
  const theme = useTheme();

  return (
    <AlertDialogPrimitive.Root>
      <AlertDialogPrimitive.Trigger asChild>{children}</AlertDialogPrimitive.Trigger>
      <AlertDialogPrimitive.Portal>
        <AlertDialogPrimitive.Overlay asChild>
          <AlertDialogOverlay {...theme.tyle.animation.fade} />
        </AlertDialogPrimitive.Overlay>
        <AlertDialogPrimitive.Content asChild>
          <AlertDialogContent variant={"elevated"} elevation={3} {...theme.tyle.animation.fade}>
            <Box display={"flex"} flexDirection={"column"} gap={theme.tyle.spacing.xxs}>
              <AlertDialogTitle hide={hideTitle}>{title}</AlertDialogTitle>
              {description && <AlertDialogDescription hide={hideDescription}>{description}</AlertDialogDescription>}
            </Box>
            {content}
            <Box display={"flex"} gap={theme.tyle.spacing.medium} m={"0 auto"}>
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
