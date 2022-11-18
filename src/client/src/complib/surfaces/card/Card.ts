import { ColorSystem } from "complib/core";
import { Polymorphic } from "complib/props";
import { motion } from "framer-motion";
import { ElementType } from "react";
import styled, { css } from "styled-components/macro";

type CardProps = Polymorphic<ElementType> & {
  variant?: "selected" | "filled";
};

export const Card = styled.div<CardProps>`
  padding: ${(props) => props.theme.tyle.spacing.xxxl};
  background-color: ${(props) => props.theme.tyle.color.sys.surface.base};
  box-shadow: ${(props) => props.theme.tyle.shadow.small};
  border-radius: ${(props) => props.theme.tyle.border.radius.large};

  ${({ variant, ...props }) => {
    const { color } = props.theme.tyle;

    if (variant === "selected") {
      return selectedCard(color);
    }
  }};
`;

Card.defaultProps = {
  variant: "filled",
};

const selectedCard = (color: ColorSystem) => css`
  background-color: ${color.sys.tertiary.container?.base};
  box-shadow: none;
`;

/**
 * An animation wrapper for the Card component
 *
 * @see https://github.com/framer/motion
 */
export const MotionCard = motion(Card);
