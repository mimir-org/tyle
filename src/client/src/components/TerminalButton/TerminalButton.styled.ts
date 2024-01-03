import { focus, layer, translucify } from "@mimirorg/component-library";
import { meetsContrastGuidelines } from "polished";
import styled, { css } from "styled-components/macro";
import { TerminalButtonProps } from "./TerminalButton";

export const TerminalButtonContainer = styled.button<TerminalButtonProps>`
  flex-shrink: 0;
  position: relative;
  display: inline-flex;
  justify-content: center;
  align-items: center;
  white-space: nowrap;
  text-decoration: none;
  padding: ${(props) => props.theme.tyle.spacing.xs};

  border: ${(props) => (props.direction ? 0 : `1px solid ${props.theme.tyle.color.outline.base}`)};
  border-radius: ${(props) => props.theme.tyle.border.radius.small};

  font: ${(props) => props.theme.tyle.typography.roles.label.large.font};
  line-height: ${(props) => props.theme.tyle.typography.roles.label.large.lineHeight};
  letter-spacing: ${(props) => props.theme.tyle.typography.roles.label.large.letterSpacing};

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
    const { color: colorSystem, elevation } = props.theme.tyle;
    const contentColor = meetsContrastGuidelines(colorSystem.background.on, color).AAA
      ? colorSystem.background.on
      : colorSystem.background.inverse.on;

    return css`
      background-color: ${color};
      color: ${contentColor};

      :disabled {
        background-color: ${translucify(colorSystem.surface.on, 0.12)};
        color: ${translucify(colorSystem.surface.on, 0.38)};
      }

      :not(:disabled) {
        :hover {
          background: ${layer(
            translucify(color, elevation.levels[1].opacity),
            translucify(colorSystem.primary.on, 0.08),
            translucify(color, 1),
          )};
        }

        :active {
          background: ${layer(translucify(colorSystem.primary.on, 0.12), translucify(color, 1))};
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
          padding: ${props.theme.tyle.spacing.s};

          path {
            stroke-width: 2;
          }
        `;
      }
    }
  }};
`;

TerminalButtonContainer.defaultProps = {
  variant: "medium",
};
