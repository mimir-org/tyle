import { Palette } from "complib/props";
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
  border-radius: ${(props) => props.theme.tyle.border.radius.large};
  border: 1px solid ${(props) => props.theme.tyle.color.sys.outline.base};
  background-color: ${(props) => props.theme.tyle.color.sys.surface.base};

  ${({ variant }) => {
    switch (variant) {
      case "small": {
        return css`
          width: 210px;
          min-height: 100px;
        `;
      }
      case "large": {
        return css`
          width: 255px;
          min-height: 150px;
        `;
      }
    }
  }};
`;

export const TransportPreviewHeader = styled.div<Pick<Palette, "bgColor">>`
  width: 100%;
  border-top-left-radius: inherit;
  border-top-right-radius: inherit;
  background-color: ${(props) => props.bgColor};
  padding: ${(props) => props.theme.tyle.spacing.base};
  padding-left: ${(props) => props.theme.tyle.spacing.xl};
  padding-right: ${(props) => props.theme.tyle.spacing.xl};
`;

export const Transport = styled(TransportIcon)`
  margin: auto 0;
`;
