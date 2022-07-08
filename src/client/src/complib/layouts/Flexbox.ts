import { motion } from "framer-motion";
import { ElementType } from "react";
import styled from "styled-components/macro";
import { flexMixin, focus } from "../mixins";
import { Flex, Polymorphic } from "../props";

type FlexboxProps = Flex & Polymorphic<ElementType>;

/**
 * A polymorphic layout component for flexbox behaviour.
 *
 * @param as polymorphic parameter for changing base element (defaults to <div>)
 * @param props can receive all standard css flexbox properties
 * @constructor
 */
export const Flexbox = styled.div<FlexboxProps>`
  display: flex;
  ${focus};
  ${flexMixin};
`;

/**
 * An animation wrapper for the Flexbox component
 *
 * @see https://github.com/framer/motion
 */
export const MotionFlexbox = motion(Flexbox, { forwardMotionProps: true });
