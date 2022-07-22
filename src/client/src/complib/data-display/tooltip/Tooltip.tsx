import * as TooltipPrimitive from "@radix-ui/react-tooltip";
import { AnimatePresence } from "framer-motion";
import { PropsWithChildren, ReactNode } from "react";
import { useTheme } from "styled-components";
import { Text } from "../../text";
import { MotionTooltipContent, TooltipContentProps } from "./Tooltip.styled";

type Props = TooltipContentProps & {
  content: ReactNode;
  placement?: "top" | "right" | "bottom" | "left";
  align?: "start" | "center" | "end";
  delay?: number;
  offset?: number;
};

/**
 * A generic tooltip for describing focusable elements.
 * Handles focus management, collision detection, a11y tags and more.
 *
 * See documentation link below for details.
 * @see https://www.radix-ui.com/docs/primitives/components/tooltip
 *
 * @param children element which receive focus to trigger tooltip
 * @param content of the tooltip itself
 * @param placement target placement of the tooltip
 * @param align target alignment of the tooltip
 * @param delay in ms before showing the tooltip
 * @param offset in px away from the element which triggers the tooltip
 * @param delegated receives sizing props for overriding default styles
 * @constructor
 */
export const Tooltip = ({
  children,
  content,
  placement = "top",
  align = "center",
  delay = 0,
  offset = 8,
  ...delegated
}: PropsWithChildren<Props>) => {
  const theme = useTheme();
  const containsTextOnly = typeof content === "string";

  return (
    <TooltipPrimitive.Root delayDuration={delay}>
      <TooltipPrimitive.Trigger asChild>{children}</TooltipPrimitive.Trigger>
      <AnimatePresence>
        <TooltipPrimitive.Content asChild avoidCollisions sideOffset={offset} side={placement} align={align}>
          <MotionTooltipContent {...theme.tyle.animation.scale} {...delegated}>
            {containsTextOnly ? <Text variant={"body-medium"}>{content}</Text> : content}
          </MotionTooltipContent>
        </TooltipPrimitive.Content>
      </AnimatePresence>
    </TooltipPrimitive.Root>
  );
};
