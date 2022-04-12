import { ElementType, PropsWithChildren } from "react";
import { Flexbox as FlexInterface } from "../props";
import { FlexboxContainer } from "./Flexbox.styled";

export interface FlexboxProps<T extends ElementType> extends FlexInterface {
  as?: T;
}

/**
 * A polymorphic layout component for flexbox behaviour.
 *
 * @param props can receive all standard css flexbox properties in addition to the "as" prop
 * @constructor
 */
export function Flexbox<T extends ElementType>(props: PropsWithChildren<FlexboxProps<T>>) {
  const { as, children, ...rest } = props;

  const Component = as || "div";
  return (
    <FlexboxContainer {...rest} as={Component}>
      {children}
    </FlexboxContainer>
  );
}
