import * as TooltipPrimitive from "@radix-ui/react-tooltip";
import merge from "lodash.merge";
import { PropsWithChildren, ReactNode } from "react";
import { useTheme } from "styled-components";
import { MotionTooltipContent } from "./Tooltip.styled";
import { AnimatePresence } from "framer-motion";
import { Text } from "../../text";

interface Props {
  content: ReactNode;
  placement?: "top" | "right" | "bottom" | "left";
  delay?: number;
  offset?: number;
}

/**
 * A generic tooltip for providing focusable elements with additional information
 *
 * @param children element which receive focus to trigger tooltip
 * @param content of the tooltip itself
 * @param placement target placement of the tooltip
 * @param delay in ms before showing the tooltip
 * @param offset in px away from the element which triggers the tooltip
 * @constructor
 */
export const Tooltip = ({ children, content, placement = "top", delay = 0, offset = 8 }: PropsWithChildren<Props>) => {
  const theme = useTheme();
  const motion = merge(theme.typeLibrary.animation.fade, theme.typeLibrary.animation.scale);
  const containsTextOnly = typeof content === "string";

  return (
    <TooltipPrimitive.Root delayDuration={delay}>
      <TooltipPrimitive.Trigger asChild>{children}</TooltipPrimitive.Trigger>
      <AnimatePresence>
        <TooltipPrimitive.Content asChild sideOffset={offset} side={placement}>
          <MotionTooltipContent {...motion}>
            {containsTextOnly ? <Text variant={"body-medium"}>{content}</Text> : content}
          </MotionTooltipContent>
        </TooltipPrimitive.Content>
      </AnimatePresence>
    </TooltipPrimitive.Root>
  );
};
