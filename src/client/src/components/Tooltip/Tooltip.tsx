import * as TooltipPrimitive from "@radix-ui/react-tooltip";
import Text from "components/Text";
import { AnimatePresence } from "framer-motion";
import { ForwardedRef, forwardRef, PropsWithChildren, ReactNode } from "react";
import { useTheme } from "styled-components";
import { Sizing } from "types/styleProps";
import { MotionTooltipContent } from "./Tooltip.styled";

export type Props = Sizing & {
  content: ReactNode;
  placement?: "top" | "right" | "bottom" | "left";
  align?: "start" | "center" | "end";
  delay?: number;
  offset?: number;
  asChild?: boolean;
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
const Tooltip = forwardRef((props: PropsWithChildren<Props>, ref: ForwardedRef<HTMLDivElement>) => {
  const { children, content, placement, align, delay, offset, asChild, ...delegated } = props;
  const theme = useTheme();
  const containsTextOnly = typeof content === "string";

  return (
    <div ref={ref}>
      <TooltipPrimitive.Root disableHoverableContent delayDuration={delay}>
        <TooltipPrimitive.Trigger asChild={asChild}>{children}</TooltipPrimitive.Trigger>
        <AnimatePresence>
          <TooltipPrimitive.Portal>
            <TooltipPrimitive.Content
              asChild={asChild}
              avoidCollisions
              sideOffset={offset}
              side={placement}
              align={align}
            >
              <MotionTooltipContent {...theme.tyle.animation.scale} {...delegated}>
                {containsTextOnly ? <Text variant={"body-medium"}>{content}</Text> : content}
              </MotionTooltipContent>
            </TooltipPrimitive.Content>
          </TooltipPrimitive.Portal>
        </AnimatePresence>
      </TooltipPrimitive.Root>
    </div>
  );
});

Tooltip.displayName = "Tooltip";
Tooltip.defaultProps = {
  placement: "top",
  align: "center",
  delay: 0,
  offset: 8,
  asChild: true,
};

export default Tooltip;
