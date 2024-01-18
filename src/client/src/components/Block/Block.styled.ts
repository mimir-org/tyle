import styled, { css } from "styled-components/macro";
import { largeBlock } from "./largeBlock";
import { smallBlock } from "./smallBlock";

export type BlockVariant = "small" | "large";

export interface BlockContainerProps {
  color?: string;
  variant?: BlockVariant;
}

const BlockContainer = styled.div<BlockContainerProps>`
  display: flex;
  flex-direction: column;
  justify-content: start;
  align-items: center;
  padding: ${(props) => props.theme.tyle.spacing.base};
  border-radius: ${(props) => props.theme.tyle.border.radius.large};
  border: ${(props) => !props.color && css`1px solid ${props.theme.tyle.color.outline.base}`};
  background: ${(props) => (props.color ? props.color : props.theme.tyle.color.surface.base)};

  ${({ variant }) => {
    switch (variant) {
      case "small": {
        return smallBlock;
      }
      case "large": {
        return largeBlock;
      }
    }
  }};
`;

export default BlockContainer;
