import { BorderSystem } from "components/TyleThemeProvider/border";
import { ColorSystem } from "components/TyleThemeProvider/color";
import { motion } from "framer-motion";
import { lighten } from "polished";
import { ButtonHTMLAttributes, ElementType } from "react";
import { flexMixin, focus, sizingMixin, spacingMixin } from "styleConstants";
import styled, { css } from "styled-components";
import { Flex, Polymorphic, Sizing, Spacings } from "types/styleProps";
import { getButtonColor } from "./Button.helpers";

export interface ButtonContainerProps
  extends Flex,
    Sizing,
    Spacings,
    Polymorphic<ElementType>,
    ButtonHTMLAttributes<HTMLButtonElement>,
    ButtonContainerPropsPartial {}

interface ButtonContainerPropsPartial {
  variant?: "filled" | "outlined" | "text" | "round";
  iconPlacement?: "left" | "right";
  iconOnly?: boolean;
  dangerousAction?: boolean;
  buttonColor?: "primary" | "success" | "warning" | "danger" | "error";
}

export const filledButton = (color: ColorSystem, dangerousAction?: boolean, buttonColor?: string) => {
  const baseColor = buttonColor ? buttonColor : color.primary.base;

  return css`
    border: 0;
    background-color: ${dangerousAction ? color.dangerousAction.base : baseColor};
    color: ${dangerousAction ? color.dangerousAction.on : color.primary.on};

    :disabled {
      background-color: ${color.outline.base};
      color: ${color.surface.variant.on};
    }

    :not(:disabled) {
      :hover {
        background-color: ${dangerousAction ? color.dangerousAction.on : color.secondary.base};
        color: ${dangerousAction ? color.dangerousAction.base : color.primary.base};
      }

      :active {
        background-color: ${color.surface.on};
        color: ${color.primary.on};
      }
    }
  `;
};

export const outlinedButton = (color: ColorSystem, dangerousAction?: boolean, buttonColor?: string) => {
  const baseColor = buttonColor ? buttonColor : color.primary.base;

  return css`
    outline: 0;
    background-color: transparent;
    border: 1px solid ${dangerousAction ? color.dangerousAction.on : baseColor};
    color: ${dangerousAction ? color.dangerousAction.on : color.primary.base};

    :disabled {
      border-color: ${color.outline.base};
      color: ${color.surface.variant.on};
    }

    :not(:disabled) {
      :hover {
        background-color: ${dangerousAction ? color.dangerousAction.on : color.secondary.base};
        color: ${dangerousAction ? color.dangerousAction.base : color.primary.base};
      }

      :active {
        background-color: ${dangerousAction ? color.dangerousAction.base : color.tertiary.container?.base};
        color: ${dangerousAction ? color.dangerousAction.on : color.primary.base};
      }
    }
  `;
};

export const roundButton = (color: ColorSystem, border: BorderSystem) => {
  return css`
    height: 50px;
    width: 50px;
    border-width: 0;
    border-radius: ${border.radius.round};
    background-color: ${color.primary.base};
    color: ${color.text.on};

    :disabled {
      background-color: ${color.outline.base};
      color: ${color.surface.variant.on};
    }

    :not(:disabled) {
      :hover {
        background-color: ${lighten(0.1, color.primary.base)};
        color: ${color.text.on};
      }

      :active {
        background-color: ${lighten(0.3, color.primary.base)};
        color: ${color.text.on};
      }
    }

    img,
    svg {
      max-width: 70%;
      max-height: 70%;
      width: 50%;
      height: 50%;
    }
  `;
};

export const textButton = (color: ColorSystem, dangerousAction?: boolean) => css`
  border: 0;
  background-color: transparent;
  color: ${dangerousAction ? color.dangerousAction.on : color.primary.base};

  :disabled {
    color: ${color.surface.variant.on};
  }

  :not(:disabled) {
    :hover {
      background-color: ${dangerousAction ? color.dangerousAction.on : color.secondary.base};
      color: ${dangerousAction ? color.dangerousAction.base : color.primary.base};
    }

    :active {
      background-color: ${color.tertiary.container?.base};
      color: ${dangerousAction ? color.dangerousAction.on : color.primary.base};
    }
  }
`;

export const ButtonContainer = styled.button<ButtonContainerProps>`
  display: inline-flex;
  justify-content: center;
  align-items: center;
  gap: ${(props) => props.theme.tyle.spacing.s};
  flex-direction: ${(props) => props.iconPlacement === "left" && "row-reverse"};

  white-space: nowrap;
  text-decoration: none;

  font: ${(props) => props.theme.tyle.typography.roles.label.large.font};
  line-height: ${(props) => props.theme.tyle.typography.roles.label.large.lineHeight};
  letter-spacing: ${(props) => props.theme.tyle.typography.roles.label.large.letterSpacing};

  height: 32px;
  width: fit-content;
  min-width: 70px;
  padding: ${(props) => props.theme.tyle.spacing.base} ${(props) => props.theme.tyle.spacing.xl};
  border-radius: ${(props) => props.theme.tyle.border.radius.medium};

  :hover {
    cursor: pointer;
  }

  :disabled {
    cursor: not-allowed;
  }

  img,
  svg {
    max-width: 24px;
    max-height: 24px;
  }

  ${focus};

  ${({ variant, dangerousAction, buttonColor, ...props }) => {
    const { color, border } = props.theme.tyle;

    switch (variant) {
      case "filled": {
        return filledButton(color, dangerousAction, getButtonColor(props.theme.tyle, buttonColor));
      }
      case "outlined": {
        return outlinedButton(color, dangerousAction, getButtonColor(props.theme.tyle, buttonColor));
      }
      case "text": {
        return textButton(color, dangerousAction);
      }
      case "round": {
        return roundButton(color, border);
      }
    }
  }};

  ${({ iconOnly, dangerousAction, ...props }) =>
    iconOnly &&
    css`
      padding: ${props.theme.tyle.spacing.xs};
      min-width: revert;
      width: 24px;
      height: 24px;

      img,
      svg {
        max-width: 18px;
        max-height: 18px;
      }
      &:hover {
        background-color: ${dangerousAction ? props.theme.tyle.color.dangerousAction.base : ""};
        color: white;
      }
    `};

  ${flexMixin};
  ${sizingMixin};
  ${spacingMixin};
`;

ButtonContainer.defaultProps = {
  variant: "filled",
};

/**
 * An animation wrapper for the ButtonContainer component
 *
 * @see https://github.com/framer/motion
 */
export const MotionButtonContainer = motion(ButtonContainer);
