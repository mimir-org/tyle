import * as AlertDialogPrimitive from "@radix-ui/react-alert-dialog";
import { Button } from "../../../buttons";

export interface AlertDialogActionItem {
  name: string;
  onAction: () => void;
}

export const AlertDialogAction = ({ name, onAction }: AlertDialogActionItem) => (
  <AlertDialogPrimitive.Action asChild>
    <Button variant={"filled"} onClick={onAction}>
      {name}
    </Button>
  </AlertDialogPrimitive.Action>
);
