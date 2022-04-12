import { ElementType, PropsWithChildren } from "react";
import { Borders, Flexbox, Palette, Shadows, Sizing, Spacing } from "../props";
import { BoxContainer } from "./Box.styled";

export interface BoxProps<T extends ElementType> extends Flexbox, Palette, Sizing, Spacing, Borders, Shadows {
  as?: T;
  display?: string;
}

/**
 * A polymorphic layout component for a box element.
 *
 * Since many components often need a generic box for layout purposes this component exposes flexbox properties
 * in addition to properties related to the palette, sizing, spacing, borders, shadows etc.
 *
 * @param props can receive all the aforementioned css properties in addition to the "as" prop
 * @constructor
 */
export function Box<T extends ElementType>(props: PropsWithChildren<BoxProps<T>>) {
  const { as, children, ...rest } = props;

  const Component = as || "div";
  return (
    <BoxContainer {...rest} as={Component}>
      {children}
    </BoxContainer>
  );
}
