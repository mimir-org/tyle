import { motion } from "framer-motion";
import { ElementType } from "react";
import {
  bordersMixin,
  displayMixin,
  flexMixin,
  focus,
  gridMixin,
  paletteMixin,
  positionsMixin,
  shadowsMixin,
  sizingMixin,
  spacingMixin,
} from "styleConstants";
import styled from "styled-components";
import {
  Borders,
  Display,
  Flex,
  Grid,
  Palette,
  Polymorphic,
  Positions,
  Shadows,
  Sizing,
  Spacings,
} from "types/styleProps";

export type BoxProps = Display &
  Positions &
  Flex &
  Grid &
  Palette &
  Sizing &
  Spacings &
  Borders &
  Shadows &
  Polymorphic<ElementType>;

/**
 * A polymorphic layout component for a box element.
 *
 * Since many components often need a generic box for layout purposes this component exposes flexbox and grid properties
 * in addition to properties related to display, positions, palette, sizing, spacing, borders, shadows etc.
 *
 * @param as polymorphic parameter for changing base element (defaults to <div>)
 * @param props can receive all the css properties related to the aforementioned interfaces: palette, sizing etc...
 * @constructor
 */
const Box = styled.div<BoxProps>`
  ${focus};
  ${displayMixin};
  ${flexMixin};
  ${gridMixin};
  ${positionsMixin};
  ${sizingMixin};
  ${bordersMixin};
  ${paletteMixin};
  ${shadowsMixin};
  ${spacingMixin};
`;

export default Box;

/**
 * An animation wrapper for the Box component
 *
 * @see https://github.com/framer/motion
 */
export const MotionBox = motion(Box);
