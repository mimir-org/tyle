import * as AlertDialogPrimitive from "@radix-ui/react-alert-dialog";
import { Button } from "complib/buttons";

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
