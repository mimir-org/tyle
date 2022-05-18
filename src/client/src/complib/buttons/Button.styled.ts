import styled, { css } from "styled-components/macro";
import { Polymorphic } from "../props";
import { ButtonHTMLAttributes, ElementType } from "react";
import { layer, translucify } from "../mixins";
import { ColorSystem, ElevationSystem, StateSystem } from "../core";

export type ButtonContainerProps = Polymorphic<ElementType> &
  ButtonHTMLAttributes<HTMLButtonElement> & {
    variant?: "filled" | "outlined" | "text";
    iconPlacement?: "left" | "right";
    iconOnly?: boolean;
    danger?: boolean;
  };

export const ButtonContainer = styled.button<ButtonContainerProps>`
  display: inline-flex;
  justify-content: center;
  align-items: center;
  gap: ${(props) => props.theme.tyle.spacing.xs};
  flex-direction: ${(props) => props.iconPlacement === "left" && "row-reverse"};
  flex-shrink: 0;

  min-width: 40px;
  min-height: 40px;
  white-space: nowrap;
  text-decoration: none;

  font: ${(props) => props.theme.tyle.typography.sys.roles.label.large.font};
  line-height: ${(props) => props.theme.tyle.typography.sys.roles.label.large.lineHeight};
  letter-spacing: ${(props) => props.theme.tyle.typography.sys.roles.label.large.letterSpacing};

  padding: ${(props) => props.theme.tyle.spacing.xs} ${(props) => props.theme.tyle.spacing.small};
  border-radius: 999px;

  :hover {
    cursor: pointer;
  }

  :disabled {
    cursor: not-allowed;
  }

  ${({ variant, danger, ...props }) => {
    const { color, state, elevation } = props.theme.tyle;

    switch (variant) {
      case "filled": {
        return filledButton(color, state, elevation, danger);
      }
      case "outlined": {
        return outlinedButton(color, state, danger);
      }
      case "text": {
        return textButton(color, state, danger);
      }
    }
  }};

  img,
  svg {
    flex: 0;
    min-width: 20px;
    min-height: 20px;
  }
`;

ButtonContainer.defaultProps = {
  variant: "filled",
};

const filledButton = (color: ColorSystem, state: StateSystem, elevationSystem: ElevationSystem, danger?: boolean) => {
  const baseColor = danger ? color.error.base : color.primary.base;
  const onBaseColor = danger ? color.error.on : color.primary.on;

  return css`
    border: 0;
    background-color: ${baseColor};
    color: ${onBaseColor};

    :disabled {
      background-color: ${translucify(color.surface.on, state.disabled.container.opacity)};
      color: ${translucify(color.surface.on, state.disabled.content.opacity)};
    }

    :not(:disabled) {
      :hover {
        background: ${layer(
          translucify(baseColor, elevationSystem.levels[1].opacity),
          translucify(onBaseColor, state.hover.opacity),
          translucify(baseColor, state.enabled.opacity)
        )};
      }

      :active {
        background: ${layer(
          translucify(onBaseColor, state.pressed.opacity),
          translucify(baseColor, state.enabled.opacity)
        )};
      }
    }
  `;
};

const outlinedButton = (color: ColorSystem, state: StateSystem, danger?: boolean) => {
  const baseColor = danger ? color.error.base : color.primary.base;
  const borderColor = danger ? color.error.container : color.outline.base;

  return css`
    background-color: transparent;
    border: 1px solid ${borderColor};
    color: ${baseColor};

    :disabled {
      border-color: ${translucify(color.surface.on, state.disabled.container.opacity)};
      color: ${translucify(color.surface.on, state.disabled.content.opacity)};
    }

    :not(:disabled) {
      :hover {
        background-color: ${translucify(baseColor, state.hover.opacity)};
      }

      :active {
        background-color: ${translucify(baseColor, state.pressed.opacity)};
      }
    }
  `;
};

const textButton = (color: ColorSystem, state: StateSystem, danger?: boolean) => {
  const baseColor = danger ? color.error.base : color.primary.base;

  return css`
    border: 0;
    background-color: transparent;
    color: ${baseColor};

    :disabled {
      color: ${translucify(color.surface.on, state.disabled.content.opacity)};
    }

    :not(:disabled) {
      :hover {
        background-color: ${translucify(baseColor, state.hover.opacity)};
      }

      :active {
        background-color: ${translucify(baseColor, state.pressed.opacity)};
      }
    }
  `;
};
