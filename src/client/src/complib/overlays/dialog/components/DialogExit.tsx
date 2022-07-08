import * as DialogPrimitive from "@radix-ui/react-dialog";
import { X } from "@styled-icons/heroicons-outline";
import { useTheme } from "styled-components";
import { TextResources } from "../../../../assets/text";
import { Button } from "../../../buttons";
import { Box } from "../../../layouts";

export const DialogExit = () => {
  const theme = useTheme();
  return (
    <DialogPrimitive.Close asChild>
      <Box position={"absolute"} top={theme.tyle.spacing.xl} right={theme.tyle.spacing.xl}>
        <Button variant={"text"} icon={<X />} iconOnly>
          {TextResources.DIALOG_CLOSE}
        </Button>
      </Box>
    </DialogPrimitive.Close>
  );
};
