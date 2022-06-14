import * as AlertDialogPrimitive from "@radix-ui/react-alert-dialog";
import { Button } from "../../../buttons";
import { TextResources } from "../../../../assets/text";

export const AlertDialogCancel = () => (
  <AlertDialogPrimitive.Cancel asChild>
    <Button variant={"outlined"}>{TextResources.ALERT_DIALOG_CLOSE}</Button>
  </AlertDialogPrimitive.Cancel>
);
