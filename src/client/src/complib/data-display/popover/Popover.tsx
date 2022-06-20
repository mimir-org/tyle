import * as PopoverPrimitive from "@radix-ui/react-popover";
import { AnimatePresence } from "framer-motion";
import merge from "lodash.merge";
import { PropsWithChildren, ReactNode } from "react";
import { useTheme } from "styled-components";
import { MotionPopoverContent } from "./Popover.styled";

interface Props {
  content: ReactNode;
  placement?: "top" | "right" | "bottom" | "left";
  align?: "start" | "center" | "end";
  offset?: number;
}

/**
 * A generic popover for providing focusable elements with extra information.
 * Handles focus management, collision detection, a11y tags and more.
 *
 * See documentation link below for details.
 * @see https://www.radix-ui.com/docs/primitives/components/popover
 *
 * @param children element which receive focus to trigger popover
 * @param content of the popover itself
 * @param placement target placement of the popover
 * @param align target alignment of the popover
 * @param offset in px away from the element which triggers the popover
 * @constructor
 */
export const Popover = ({
  children,
  content,
  placement = "top",
  align = "center",
  offset = 8,
}: PropsWithChildren<Props>) => {
  const theme = useTheme();
  const motion = merge({}, theme.tyle.animation.fade, theme.tyle.animation.scale);

  return (
    <PopoverPrimitive.Root>
      <PopoverPrimitive.Trigger asChild>{children}</PopoverPrimitive.Trigger>
      <AnimatePresence>
        <PopoverPrimitive.Content asChild avoidCollisions sideOffset={offset} side={placement} align={align}>
          <MotionPopoverContent {...motion}>{content}</MotionPopoverContent>
        </PopoverPrimitive.Content>
      </AnimatePresence>
    </PopoverPrimitive.Root>
  );
};
