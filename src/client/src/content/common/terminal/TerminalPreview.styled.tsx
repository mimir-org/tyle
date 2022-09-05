import styled, { css } from "styled-components/macro";

export type TerminalPreviewVariant = "small" | "large";

export interface TerminalPreviewContainerProps {
  variant?: TerminalPreviewVariant;
}

export const TerminalPreviewContainer = styled.div<TerminalPreviewContainerProps>`
  display: flex;
  flex-direction: column;
  justify-content: start;
  align-items: center;
  padding: ${(props) => props.theme.tyle.spacing.xl};
  border-radius: ${(props) => props.theme.tyle.border.radius.large};
  border: 1px solid ${(props) => props.theme.tyle.color.sys.outline.base};
  background-color: ${(props) => props.theme.tyle.color.sys.surface.base};
  max-width: 255px;

  ${({ variant, ...props }) => {
    switch (variant) {
      case "small": {
        return css`
          gap: ${props.theme.tyle.spacing.l};
          width: 210px;
          min-height: 100px;
        `;
      }
      case "large": {
        return css`
          gap: ${props.theme.tyle.spacing.xxxl};
          width: 255px;
          min-height: 150px;
        `;
      }
    }
  }};
`;
