import styled from "styled-components/macro";
import { ElementType } from "react";
import { motion } from "framer-motion";
import { Grid as GridInterface, Polymorphic } from "../props";
import { gridMixin } from "../mixins";

type GridProps = GridInterface & Polymorphic<ElementType>;

/**
 * A polymorphic layout component for grid behaviour.
 *
 * @param as polymorphic parameter for changing base element (defaults to <div>)
 * @param props can receive all standard css grid properties
 * @constructor
 */
export const Grid = styled.div<GridProps>`
  display: grid;
  ${gridMixin}
`;

/**
 * An animation wrapper for the Grid component
 *
 * @see https://github.com/framer/motion
 */
export const MotionGrid = motion(Grid, { forwardMotionProps: true });
