import { darken } from "polished";
import styled, { css } from "styled-components/macro";
import { layer, translucify } from "../../../../../../complib/mixins";
import { AttributeButtonProps } from "./AttributeButton";

export const AttributeButtonContainer = styled.button<AttributeButtonProps>`
  flex-shrink: 0;
  text-decoration: none;
  padding: 0 ${(props) => props.theme.tyle.spacing.xxs};
  border: 0;

  :hover {
    cursor: pointer;
  }

  :disabled {
    cursor: not-allowed;
  }

  ${({ ...props }) => {
    const { color: colorSystem, state, elevation } = props.theme.tyle;
    const backgroundColor = props.theme.tyle.color.sys.tertiary.base;
    const color = props.theme.tyle.color.sys.tertiary.on;
    const borderColor = darken(0.05, backgroundColor);

    return css`
      background-color: ${backgroundColor};
      color: ${color};
      border: 2px solid ${borderColor};

      :disabled {
        border-color: ${translucify(colorSystem.sys.outline.base, state.disabled.container.opacity)};
        background-color: ${translucify(colorSystem.sys.surface.on, state.disabled.container.opacity)};
        color: ${translucify(colorSystem.sys.surface.on, state.disabled.content.opacity)};
      }

      :not(:disabled) {
        :hover {
          background: ${layer(
            translucify(backgroundColor, elevation.levels[1].opacity),
            translucify(colorSystem.sys.primary.on, state.hover.opacity),
            translucify(backgroundColor, state.enabled.opacity)
          )};
        }

        :active {
          background: ${layer(
            translucify(colorSystem.sys.primary.on, state.pressed.opacity),
            translucify(backgroundColor, state.enabled.opacity)
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
      case "small": {
        return smallButton();
      }
    }
  }};
};
`;

const largeButton = () => css`
  width: 110px;
  height: 50px;
`;

const mediumButton = () => css`
  width: 85px;
  height: 40px;
`;

const smallButton = () => css`
  width: 65px;
  height: 35px;
`;
