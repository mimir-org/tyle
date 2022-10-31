import { gridMixin } from "complib/mixins";
import { Grid as GridInterface, Polymorphic } from "complib/props";
import { motion } from "framer-motion";
import { ElementType } from "react";
import styled from "styled-components/macro";

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
export const MotionGrid = motion(Grid);
