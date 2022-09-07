import styled, { css } from "styled-components/macro";
import { TransportIcon } from "../../../assets/icons/transport";

export type TransportPreviewVariant = "small" | "large";

export interface TransportPreviewContainerProps {
  variant?: TransportPreviewVariant;
}

export const TransportPreviewContainer = styled.div<TransportPreviewContainerProps>`
  display: flex;
  flex-direction: column;
  align-items: center;
  align-self: center;
  padding: ${(props) => props.theme.tyle.spacing.xl};
  border-radius: ${(props) => props.theme.tyle.border.radius.large};
  border: 1px solid ${(props) => props.theme.tyle.color.sys.outline.base};
  background-color: ${(props) => props.theme.tyle.color.sys.surface.base};

  ${({ variant, ...props }) => {
    switch (variant) {
      case "small": {
        return css`
          gap: ${props.theme.tyle.spacing.xl};
          width: 210px;
          min-height: 100px;
        `;
      }
      case "large": {
        return css`
          gap: ${props.theme.tyle.spacing.multiple(4)};
          width: 255px;
          min-height: 150px;
        `;
      }
    }
  }};
`;

export const Transport = styled(TransportIcon)``;
