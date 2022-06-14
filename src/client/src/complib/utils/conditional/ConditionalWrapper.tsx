import { ReactElement } from "react";

interface ConditionalWrapperProps {
  condition?: boolean;
  wrapper: (children: ReactElement) => ReactElement;
  children: ReactElement;
}

/**
 * Component which facilities conditional wrapping of its children
 *
 * @example
 * // In this example if hideTitle is true <DialogTitle/> will be wrapped inside <VisuallyHidden/>
 * <ConditionalWrapper condition={hideTitle} wrapper={(c) => <VisuallyHidden asChild>{c}</VisuallyHidden>}>
 *  <DialogTitle>{title}</DialogTitle>
 * </ConditionalWrapper>
 *
 * @param condition that decides if children should be wrapped
 * @param wrapper the wrapping component
 * @param children component that will be wrapped if condition equals true
 * @constructor
 */
export const ConditionalWrapper = ({ condition, wrapper, children }: ConditionalWrapperProps) => {
  return condition ? wrapper(children) : children;
};
