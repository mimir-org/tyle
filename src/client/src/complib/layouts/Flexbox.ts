import styled from "styled-components/macro";
import { ElementType } from "react";
import { motion } from "framer-motion";
import { Flexbox as FlexboxInterface, Polymorphic } from "../props";

type FlexboxProps = FlexboxInterface & Polymorphic<ElementType>;

/**
 * A polymorphic layout component for flexbox behaviour.
 *
 * @param as polymorphic parameter for changing base element (defaults to <div>)
 * @param props can receive all standard css flexbox properties
 * @constructor
 */
export const Flexbox = styled.div<FlexboxProps>`
  display: flex;
  flex-direction: ${(props) => props.flexDirection};
  flex-wrap: ${(props) => props.flexWrap};
  justify-content: ${(props) => props.justifyContent};
  align-items: ${(props) => props.alignItems};
  align-content: ${(props) => props.alignContent};
  order: ${(props) => props.order};
  flex: ${(props) => props.flex};
  flex-grow: ${(props) => props.flexGrow};
  flex-shrink: ${(props) => props.flexShrink};
  align-self: ${(props) => props.alignSelf};
  gap: ${(props) => props.gap};
`;

/**
 * An animation wrapper for the Flexbox component
 *
 * @see https://github.com/framer/motion
 */
export const MotionFlexbox = motion(Flexbox, { forwardMotionProps: true });
