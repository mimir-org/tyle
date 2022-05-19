import styled, { css } from "styled-components/macro";
import { layer, translucify } from "../../../../../../complib/mixins";
import { ButtonHTMLAttributes } from "react";
import { Plus, SwitchHorizontal } from "@styled-icons/heroicons-outline";

export type TerminalButtonContainerProps = ButtonHTMLAttributes<HTMLButtonElement> & {
  color: string;
  size?: number;
};

export const TerminalButtonContainer = styled.button<TerminalButtonContainerProps>`
  flex-shrink: 0;
  position: relative;
  display: inline-flex;
  justify-content: center;
  align-items: center;
  gap: ${(props) => props.theme.tyle.spacing.xs};
  white-space: nowrap;
  text-decoration: none;
  padding: ${(props) => props.theme.tyle.spacing.xxs};
  
  width: ${(props) => `${props.size}px`};
  height: ${(props) => `${props.size}px`};

  font: ${(props) => props.theme.tyle.typography.sys.roles.label.large.font};
  line-height: ${(props) => props.theme.tyle.typography.sys.roles.label.large.lineHeight};
  letter-spacing: ${(props) => props.theme.tyle.typography.sys.roles.label.large.letterSpacing};

  :hover {
    cursor: pointer;
  }

  :disabled {
    cursor: not-allowed;
  }

  ${({ color, ...props }) => {
    const { color: colorSystem, state, elevation } = props.theme.tyle;

    return css`
      border: 0;
      background-color: ${color};
      color: #ffffff;

      :disabled {
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
};
`;

TerminalButtonContainer.defaultProps = {
  size: 25,
};

export const ThickPlus = styled(Plus)`
  path {
    stroke-width: 3;
  }
`;

export const ThickSwitchHorizontal = styled(SwitchHorizontal)`
  path {
    stroke-width: 3;
  }
`;
