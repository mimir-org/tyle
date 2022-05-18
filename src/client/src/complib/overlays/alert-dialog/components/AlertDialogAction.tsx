import * as AlertDialogPrimitive from "@radix-ui/react-alert-dialog";
import { Button } from "../../../buttons";

export interface AlertDialogActionItem {
  name: string;
  onAction: () => void;
  danger?: boolean;
}

export const AlertDialogAction = ({ name, onAction, danger }: AlertDialogActionItem) => (
  <AlertDialogPrimitive.Action asChild>
    <Button variant={"filled"} onClick={onAction} danger={danger}>
      {name}
    </Button>
  </AlertDialogPrimitive.Action>
);
