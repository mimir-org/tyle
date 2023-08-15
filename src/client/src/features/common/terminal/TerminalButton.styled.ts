import { focus, layer, translucify } from "@mimirorg/component-library";
import { TerminalButtonProps } from "features/common/terminal/TerminalButton";
import { meetsContrastGuidelines } from "polished";
import styled, { css } from "styled-components/macro";

export const TerminalButtonContainer = styled.button<TerminalButtonProps>`
  flex-shrink: 0;
  position: relative;
  display: inline-flex;
  justify-content: center;
  align-items: center;
  white-space: nowrap;
  text-decoration: none;
  padding: ${(props) => props.theme.mimirorg.spacing.xs};

  border: ${(props) => (props.direction ? 0 : `1px solid ${props.theme.mimirorg.color.outline.base}`)};
  border-radius: ${(props) => props.theme.mimirorg.border.radius.small};

  font: ${(props) => props.theme.mimirorg.typography.roles.label.large.font};
  line-height: ${(props) => props.theme.mimirorg.typography.roles.label.large.lineHeight};
  letter-spacing: ${(props) => props.theme.mimirorg.typography.roles.label.large.letterSpacing};

  :hover {
    cursor: default;
  }

  :disabled {
    cursor: not-allowed;
  }

  path {
    stroke-width: 3;
  }

  ${focus};

  ${({ color, ...props }) => {
    const { color: colorSystem, state, elevation } = props.theme.mimirorg;
    const contentColor = meetsContrastGuidelines(colorSystem.background.on, color).AAA
      ? colorSystem.background.on
      : colorSystem.background.inverse.on;

    return css`
      background-color: ${color};
      color: ${contentColor};

      :disabled {
        background-color: ${translucify(colorSystem.surface.on, state.disabled.container.opacity)};
        color: ${translucify(colorSystem.surface.on, state.disabled.content.opacity)};
      }

      :not(:disabled) {
        :hover {
          background: ${layer(
            translucify(color, elevation.levels[1].opacity),
            translucify(colorSystem.primary.on, state.hover.opacity),
            translucify(color, state.enabled.opacity),
          )};
        }

        :active {
          background: ${layer(
            translucify(colorSystem.primary.on, state.pressed.opacity),
            translucify(color, state.enabled.opacity),
          )};
        }
      }
    `;
  }};

  ${({ variant, ...props }) => {
    switch (variant) {
      case "small": {
        return css`
          width: 15px;
          height: 15px;
        `;
      }
      case "medium": {
        return css`
          width: 18px;
          height: 18px;
        `;
      }
      case "large": {
        return css`
          width: 30px;
          height: 30px;
          padding: ${props.theme.mimirorg.spacing.s};

          path {
            stroke-width: 2;
          }
        `;
      }
    }
  }};
}

;
`;

TerminalButtonContainer.defaultProps = {
  variant: "medium",
};
