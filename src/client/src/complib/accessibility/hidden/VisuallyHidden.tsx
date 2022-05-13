import { PropsWithChildren } from "react";
import * as VisuallyHiddenPrimitive from "@radix-ui/react-visually-hidden";

interface Props {
  asChild?: boolean;
}

/**
 * A component for including data without displaying it.
 * A typical use case might be description text for icons that can only be read by screen readers.
 *
 * @param children Components or raw text that should remain hidden.
 * @param asChild Change the component to the HTML tag or custom component of the only child. This will merge the
 * original component props with the props of the supplied element/component and change the underlying DOM node.
 * @constructor
 */
export const VisuallyHidden = ({ children, asChild }: PropsWithChildren<Props>) => {
  return <VisuallyHiddenPrimitive.Root asChild={asChild}>{children}</VisuallyHiddenPrimitive.Root>;
};
