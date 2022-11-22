import * as AlertDialogPrimitive from "@radix-ui/react-alert-dialog";
import { Button } from "complib/buttons";

export const AlertDialogCancel = ({ cancelText }: { cancelText?: string }) => (
  <AlertDialogPrimitive.Cancel asChild>
    <Button variant={"outlined"}>{cancelText ?? "Cancel"}</Button>
  </AlertDialogPrimitive.Cancel>
);
