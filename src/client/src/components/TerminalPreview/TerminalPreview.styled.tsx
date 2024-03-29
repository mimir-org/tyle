import styled, { css } from "styled-components/macro";

export type TerminalPreviewVariant = "small" | "large";

export interface TerminalPreviewContainerProps {
  variant?: TerminalPreviewVariant;
}

const TerminalPreviewContainer = styled.div<TerminalPreviewContainerProps>`
  display: flex;
  flex-direction: column;
  align-items: center;
  align-self: center;
  padding: ${(props) => props.theme.tyle.spacing.xl};
  border-radius: ${(props) => props.theme.tyle.border.radius.large};
  border: 1px solid ${(props) => props.theme.tyle.color.outline.base};
  background-color: ${(props) => props.theme.tyle.color.pure.base};

  ${({ variant, ...props }) => {
    switch (variant) {
      case "small": {
        return css`
          gap: ${props.theme.tyle.spacing.l};
          width: 200px;
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

export default TerminalPreviewContainer;
