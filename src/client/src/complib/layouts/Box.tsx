import styled from "styled-components/macro";
import { ElementType } from "react";
import { motion } from "framer-motion";
import { Flexbox } from "./Flexbox";
import { Borders, Flexbox as FlexInterface, Palette, Polymorphic, Shadows, Sizing, Spacing } from "../props";

export interface BoxProps extends FlexInterface, Palette, Sizing, Spacing, Borders, Shadows, Polymorphic<ElementType> {
  display?: string;
}

/**
 * A polymorphic layout component for a box element.
 *
 * Since many components often need a generic box for layout purposes this component exposes flexbox properties
 * in addition to properties related to the palette, sizing, spacing, borders, shadows etc.
 *
 * @param as polymorphic parameter for changing base element (defaults to <div>)
 * @param props can receive all the css properties related to the aforementioned interfaces: palette, sizing etc...
 * @constructor
 */
export const Box = styled(Flexbox)<BoxProps>`
  display: ${(props) => props.display ?? "revert"};

  width: ${(props) => props.width};
  max-width: ${(props) => props.maxWidth};
  min-width: ${(props) => props.minWidth};
  height: ${(props) => props.height};
  max-height: ${(props) => props.maxHeight};
  min-height: ${(props) => props.minHeight};
  box-sizing: ${(props) => props.boxSizing};

  padding: ${(props) => props.p};
  padding-top: ${(props) => props.pt};
  padding-right: ${(props) => props.pr};
  padding-bottom: ${(props) => props.pb};
  padding-left: ${(props) => props.pl};

  margin: ${(props) => props.m};
  margin-top: ${(props) => props.mt};
  margin-right: ${(props) => props.mr};
  margin-bottom: ${(props) => props.mb};
  margin-left: ${(props) => props.ml};

  border: ${(props) => props.border};
  border-top: ${(props) => props.borderTop};
  border-left: ${(props) => props.borderLeft};
  border-right: ${(props) => props.borderRight};
  border-bottom: ${(props) => props.borderBottom};
  border-color: ${(props) => props.borderColor};
  border-top-color: ${(props) => props.borderTopColor};
  border-right-color: ${(props) => props.borderRightColor};
  border-bottom-color: ${(props) => props.borderBottomColor};
  border-left-color: ${(props) => props.borderLeftColor};
  border-radius: ${(props) => props.borderRadius};

  color: ${(props) => props.color};
  background-color: ${(props) => props.bgColor};

  box-shadow: ${(props) => props.boxShadow};
`;

/**
 * An animation wrapper for the Box component
 *
 * @see https://github.com/framer/motion
 */
export const MotionBox = motion(Box, { forwardMotionProps: true });
