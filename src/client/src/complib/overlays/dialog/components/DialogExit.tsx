import * as DialogPrimitive from "@radix-ui/react-dialog";
import { X } from "@styled-icons/heroicons-outline";
import { useTheme } from "styled-components";
import { Button } from "../../../buttons";
import { Box } from "../../../layouts";

export const DialogExit = ({ closeText }: { closeText?: string }) => {
  const theme = useTheme();
  return (
    <DialogPrimitive.Close asChild>
      <Box position={"absolute"} top={theme.tyle.spacing.xl} right={theme.tyle.spacing.xl}>
        <Button variant={"text"} icon={<X />} iconOnly>
          {closeText ?? "Close dialog"}
        </Button>
      </Box>
    </DialogPrimitive.Close>
  );
};
