import { motion } from "framer-motion";
import { ElementType } from "react";
import { flexMixin, focus } from "styleConstants";
import styled from "styled-components";
import { Flex, Polymorphic } from "types/styleProps";

export type FlexBoxProps = Flex & Polymorphic<ElementType>;

/**
 * A polymorphic layout component for flexbox behaviour.
 *
 * @param as polymorphic parameter for changing base element (defaults to <div>)
 * @param props can receive all standard css flexbox properties
 * @constructor
 */
const Flexbox = styled.div<FlexBoxProps>`
  display: flex;
  ${focus};
  ${flexMixin};
`;

/**
 * An animation wrapper for the Flexbox component
 *
 * @see https://github.com/framer/motion
 */
export const MotionFlexbox = motion(Flexbox);

export default Flexbox;
