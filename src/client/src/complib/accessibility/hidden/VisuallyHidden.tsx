import { PropsWithChildren } from "react";
import VisuallyHiddenSpan from "./styled/VisuallyHiddenSpan";

interface Props {
  delegated?: { [key: string]: unknown };
}

/**
 * A component for including data without displaying it.
 * A typical use case might be description text for icons that can only be read by screen readers.
 * @param children Components or raw text that should remain hidden.
 * @param delegated Extra attributes that can be set on the visually hidden containing element.
 * @constructor
 */
export const VisuallyHidden = ({ children, ...delegated }: PropsWithChildren<Props>) => {
  return <VisuallyHiddenSpan {...delegated}>{children}</VisuallyHiddenSpan>;
};
