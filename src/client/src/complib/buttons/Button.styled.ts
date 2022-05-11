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
  };

export const ButtonContainer = styled.button<ButtonContainerProps>`
  display: inline-flex;
  justify-content: center;
  align-items: center;
  gap: ${(props) => props.theme.typeLibrary.spacing.xs};
  flex-direction: ${(props) => props.iconPlacement === "left" && "row-reverse"};
  flex-shrink: 0;

  min-width: 40px;
  min-height: 40px;
  white-space: nowrap;
  text-decoration: none;

  font: ${(props) => props.theme.typeLibrary.typography.sys.roles.label.large.font};
  line-height: ${(props) => props.theme.typeLibrary.typography.sys.roles.label.large.lineHeight};
  letter-spacing: ${(props) => props.theme.typeLibrary.typography.sys.roles.label.large.letterSpacing};

  padding: ${(props) => props.theme.typeLibrary.spacing.xs} ${(props) => props.theme.typeLibrary.spacing.small};
  border-radius: 999px;

  :hover {
    cursor: pointer;
  }

  :disabled {
    cursor: not-allowed;
  }

  ${({ variant, ...props }) => {
    const { color, state, elevation } = props.theme.typeLibrary;

    switch (variant) {
      case "filled": {
        return filledButton(color, state, elevation);
      }
      case "outlined": {
        return outlinedButton(color, state);
      }
      case "text": {
        return textButton(color, state);
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

const filledButton = (color: ColorSystem, state: StateSystem, elevationSystem: ElevationSystem) => css`
  border: 0;
  background-color: ${color.primary.base};
  color: ${color.primary.on};

  :disabled {
    background-color: ${translucify(color.surface.on, state.disabled.container.opacity)};
    color: ${translucify(color.surface.on, state.disabled.content.opacity)};
  }

  :not(:disabled) {
    :hover {
      background: ${layer(
        translucify(color.primary.base, elevationSystem.levels[1].opacity),
        translucify(color.primary.on, state.hover.opacity),
        translucify(color.primary.base, state.enabled.opacity)
      )};
    }

    :active {
      background: ${layer(
        translucify(color.primary.on, state.pressed.opacity),
        translucify(color.primary.base, state.enabled.opacity)
      )};
    }
  }
`;

const outlinedButton = (color: ColorSystem, state: StateSystem) => css`
  background-color: transparent;
  border: 1px solid ${color.outline.base};
  color: ${color.primary.base};

  :disabled {
    border-color: ${translucify(color.surface.on, state.disabled.container.opacity)};
    color: ${translucify(color.surface.on, state.disabled.content.opacity)};
  }

  :not(:disabled) {
    :hover {
      background-color: ${translucify(color.primary.base, state.hover.opacity)};
    }

    :active {
      background-color: ${translucify(color.primary.base, state.pressed.opacity)};
    }
  }
`;

const textButton = (color: ColorSystem, state: StateSystem) => css`
  border: 0;
  background-color: transparent;
  color: ${color.primary.base};

  :disabled {
    color: ${translucify(color.surface.on, state.disabled.content.opacity)};
  }

  :not(:disabled) {
    :hover {
      background-color: ${translucify(color.primary.base, state.hover.opacity)};
    }

    :active {
      background-color: ${translucify(color.primary.base, state.pressed.opacity)};
    }
  }
`;
