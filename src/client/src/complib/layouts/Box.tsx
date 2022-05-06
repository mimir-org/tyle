import styled from "styled-components/macro";
import { ElementType } from "react";
import { motion } from "framer-motion";
import { Borders, Display, Flex, Grid, Palette, Polymorphic, Positions, Shadows, Sizing, Spacing } from "../props";
import {
  bordersMixin,
  displayMixin,
  flexMixin,
  gridMixin,
  paletteMixin,
  positionsMixin,
  shadowsMixin,
  sizingMixin,
  spacingMixin,
} from "../mixins";

type BoxProps = Display &
  Positions &
  Flex &
  Grid &
  Palette &
  Sizing &
  Spacing &
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
export const Box = styled.div<BoxProps>`
  ${displayMixin};
  ${flexMixin};
  ${gridMixin};
  ${positionsMixin};
  ${sizingMixin};
  ${spacingMixin};
  ${bordersMixin};
  ${paletteMixin};
  ${shadowsMixin};
`;

/**
 * An animation wrapper for the Box component
 *
 * @see https://github.com/framer/motion
 */
export const MotionBox = motion(Box, { forwardMotionProps: true });
