import * as VisuallyHiddenPrimitive from "@radix-ui/react-visually-hidden";
import { PropsWithChildren } from "react";

interface Props {
  asChild?: boolean;
}

/**
 * A component for including data without displaying it.
 * A typical use case might be description text for icons that can only be read by screen readers.
 *
 * See documentation link below for details.
 * @see https://www.radix-ui.com/docs/primitives/utilities/visually-hidden
 *
 * @param children Components or raw text that should remain hidden.
 * @param asChild Change the component to the HTML tag or custom component of the only child. This will merge the
 * original component props with the props of the supplied element/component and change the underlying DOM node.
 * @constructor
 */
export const VisuallyHidden = ({ children, asChild }: PropsWithChildren<Props>) => {
  return <VisuallyHiddenPrimitive.Root asChild={asChild}>{children}</VisuallyHiddenPrimitive.Root>;
};
