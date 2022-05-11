import * as PopoverPrimitive from "@radix-ui/react-popover";
import merge from "lodash.merge";
import { PropsWithChildren, ReactNode } from "react";
import { useTheme } from "styled-components";
import { AnimatePresence } from "framer-motion";
import { MotionPopoverContent } from "./Popover.styled";

interface Props {
  content: ReactNode;
  placement?: "top" | "right" | "bottom" | "left";
  offset?: number;
}

/**
 * A generic popover for providing focusable elements with extra information.
 * Handles focus management, collision detection, a11y tags and more.
 *
 * See documentation link below for details.
 * @see https://www.radix-ui.com/docs/primitives/components/popover
 *
 * @param children element which receive focus to trigger tooltip
 * @param content of the tooltip itself
 * @param placement target placement of the tooltip
 * @param offset in px away from the element which triggers the tooltip
 * @constructor
 */
export const Popover = ({ children, content, placement = "top", offset = 8 }: PropsWithChildren<Props>) => {
  const theme = useTheme();
  const motion = merge({}, theme.typeLibrary.animation.fade, theme.typeLibrary.animation.scale);

  return (
    <PopoverPrimitive.Root>
      <PopoverPrimitive.Trigger asChild>{children}</PopoverPrimitive.Trigger>
      <AnimatePresence>
        <PopoverPrimitive.Content asChild avoidCollisions sideOffset={offset} side={placement}>
          <MotionPopoverContent {...motion}>{content}</MotionPopoverContent>
        </PopoverPrimitive.Content>
      </AnimatePresence>
    </PopoverPrimitive.Root>
  );
};
