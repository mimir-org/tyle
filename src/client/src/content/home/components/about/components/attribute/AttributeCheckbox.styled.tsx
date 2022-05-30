import * as CheckboxPrimitive from "@radix-ui/react-checkbox";
import { darken } from "polished";
import styled, { css } from "styled-components/macro";
import { layer, translucify } from "../../../../../../complib/mixins";
import { AttributeCheckboxProps } from "./AttributeCheckbox";

export const AttributeCheckboxContainer = styled(CheckboxPrimitive.Root)<AttributeCheckboxProps>`
  flex-shrink: 0;
  text-decoration: none;
  padding: 0 ${(props) => props.theme.tyle.spacing.xxs};
  border: 0;

  outline: ${(props) => props.checked && `2px solid ${props.theme.tyle.color.background.on}`};
  width: 110px;
  height: 50px;

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
`;
