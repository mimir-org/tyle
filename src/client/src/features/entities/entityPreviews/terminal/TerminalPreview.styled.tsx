import styled, { css } from "styled-components/macro";

export type TerminalPreviewVariant = "small" | "large";

export interface TerminalPreviewContainerProps {
  variant?: TerminalPreviewVariant;
}

export const TerminalPreviewContainer = styled.div<TerminalPreviewContainerProps>`
  display: flex;
  flex-direction: column;
  align-items: center;
  align-self: center;
  padding: ${(props) => props.theme.mimirorg.spacing.xl};
  border-radius: ${(props) => props.theme.mimirorg.border.radius.large};
  border: 1px solid ${(props) => props.theme.mimirorg.color.outline.base};
  background-color: ${(props) => props.theme.mimirorg.color.pure.base};

  ${({ variant, ...props }) => {
    switch (variant) {
      case "small": {
        return css`
          gap: ${props.theme.mimirorg.spacing.l};
          width: 200px;
          min-height: 100px;
        `;
      }
      case "large": {
        return css`
          gap: ${props.theme.mimirorg.spacing.xxxl};
          width: 255px;
          min-height: 150px;
        `;
      }
    }
  }};
`;
