import { flexMixin, focus } from "complib/mixins";
import { Flex, Polymorphic } from "complib/props";
import { motion } from "framer-motion";
import { ElementType } from "react";
import styled from "styled-components/macro";

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
export const MotionFlexbox = motion(Flexbox);
