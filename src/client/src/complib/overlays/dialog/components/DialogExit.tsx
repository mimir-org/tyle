import * as DialogPrimitive from "@radix-ui/react-dialog";
import { XMark } from "@styled-icons/heroicons-outline";
import { Button } from "complib/buttons";
import { Box } from "complib/layouts";
import { useTheme } from "styled-components";

export const DialogExit = ({ closeText }: { closeText?: string }) => {
  const theme = useTheme();
  return (
    <DialogPrimitive.Close asChild>
      <Box position={"absolute"} top={theme.tyle.spacing.xl} right={theme.tyle.spacing.xl}>
        <Button variant={"text"} icon={<XMark />} iconOnly>
          {closeText ?? "Close dialog"}
        </Button>
      </Box>
    </DialogPrimitive.Close>
  );
};
