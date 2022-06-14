import * as DialogPrimitive from "@radix-ui/react-dialog";
import { useTheme } from "styled-components";
import { Button } from "../../../buttons";
import { Box } from "../../../layouts";
import { X } from "@styled-icons/heroicons-outline";
import { TextResources } from "../../../../assets/text";

export const DialogExit = () => {
  const theme = useTheme();
  return (
    <DialogPrimitive.Close asChild>
      <Box position={"absolute"} top={theme.tyle.spacing.xxs} right={theme.tyle.spacing.xxs}>
        <Button variant={"text"} icon={<X />} iconOnly>
          {TextResources.DIALOG_CLOSE}
        </Button>
      </Box>
    </DialogPrimitive.Close>
  );
};
