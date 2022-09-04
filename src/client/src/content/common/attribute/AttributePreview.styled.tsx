import { motion } from "framer-motion";
import styled, { css } from "styled-components/macro";
import { Divider } from "../../../complib/data-display";
import { Palette } from "../../../complib/props";

export type AttributePreviewVariant = "small" | "large";

export interface AttributePreviewContainerProps {
  variant?: AttributePreviewVariant;
}

export const AttributePreviewContainer = styled.div<AttributePreviewContainerProps>`
  display: flex;
  flex-direction: column;
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

export const AttributePreviewHeader = styled.div<Pick<Palette, "bgColor">>`
  width: 100%;
  border-top-left-radius: inherit;
  border-top-right-radius: inherit;
  background-color: ${(props) => props.bgColor};
  padding: ${(props) => props.theme.tyle.spacing.base};
  padding-left: ${(props) => props.theme.tyle.spacing.xl};
  padding-right: ${(props) => props.theme.tyle.spacing.xl};
`;

export const AttributeSpecificationContainer = styled.div`
  flex: 1;
  display: flex;
  flex-direction: column;
  justify-content: space-evenly;
  gap: ${(props) => props.theme.tyle.spacing.xl};
  padding: ${(props) => props.theme.tyle.spacing.l};
  padding-left: ${(props) => props.theme.tyle.spacing.xl};
  padding-right: ${(props) => props.theme.tyle.spacing.xl};
`;

export const AttributeSpecificationGrid = styled(motion.div)`
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  gap: ${(props) => props.theme.tyle.spacing.s};
  text-transform: capitalize;
`;

export const AttributeSpecificationGridDivider = styled(Divider)`
  background-color: ${(props) => props.theme.tyle.color.sys.background.on};
  grid-column: 1 / 4;
`;
