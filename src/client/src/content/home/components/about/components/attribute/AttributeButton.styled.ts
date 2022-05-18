import styled, { css } from "styled-components/macro";
import { layer, translucify } from "../../../../../../complib/mixins";
import { ButtonHTMLAttributes } from "react";
import { darken } from "polished";

export type AttributeButtonContainerProps = ButtonHTMLAttributes<HTMLButtonElement> & {
  color: string;
  variant?: "large" | "medium";
};

export const AttributeButtonContainer = styled.button<AttributeButtonContainerProps>`
  text-decoration: none;
  padding: ${(props) => props.theme.tyle.spacing.xxs};
  border: 0;

  :hover {
    cursor: pointer;
  }

  :disabled {
    cursor: not-allowed;
  }

  ${({ color, ...props }) => {
    const { color: colorSystem, state, elevation } = props.theme.tyle;
    const borderColor = darken(0.05, color);

    return css`
      background-color: ${color};
      color: #ffffff;
      border: 2px solid ${borderColor};

      :disabled {
        border-color: ${translucify(colorSystem.outline.base, state.disabled.container.opacity)};
        background-color: ${translucify(colorSystem.surface.on, state.disabled.container.opacity)};
        color: ${translucify(colorSystem.surface.on, state.disabled.content.opacity)};
      }

      :not(:disabled) {
        :hover {
          background: ${layer(
            translucify(color, elevation.levels[1].opacity),
            translucify(colorSystem.primary.on, state.hover.opacity),
            translucify(color, state.enabled.opacity)
          )};
        }

        :active {
          background: ${layer(
            translucify(colorSystem.primary.on, state.pressed.opacity),
            translucify(color, state.enabled.opacity)
          )};
        }
      }
    `;
  }};

  ${({ variant }) => {
    switch (variant) {
      case "large": {
        return largeButton();
      }
      case "medium": {
        return mediumButton();
      }
    }
  }};
};
`;

const largeButton = () => css`
  width: 110px;
  height: 40px;
`;

const mediumButton = () => css`
  width: 90px;
  height: 35px;
`;
